using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Server.Hubs;
using HelpDesk.Server.Repository;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IMensajeChatRepository _mensajeChatRepository;
        private readonly IGrupoChatRepository _grupoChatRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioChatRepository _usuarioChatRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private IHubContext<ChatHub> _chatHubContext;

        public ChatController(HelpDeskContext context, IHubContext<ChatHub> chatHubContext)
        {
            this._mensajeChatRepository = new MensajeChatRepository(context);
            this._usuarioRepository = new UsuarioRepository(context);
            this._usuarioChatRepository = new UsuarioChatRepository(context);
            this._departamentoRepository = new DepartamentoRepository(context);
            this._grupoChatRepository = new GrupoChatRepository(context);
            this._chatHubContext = chatHubContext;
        }

        /// <summary>
        /// Traer los usuarios para el chat
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetGrupos")]
        public async Task<ActionResult<GruposChat>> GetGrupos()
        {
            ICollection<Usuario> usuarios = await _usuarioRepository.GetUsuarios();
            ICollection<Departamento> departamentos = await _departamentoRepository.GetDepartamentos();

            var usuario = await _usuarioRepository.GetUsuarioPorId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));


            ICollection<UsuarioChat> usuariosChat = usuarios.Join(departamentos, u => u.DepartamentoId, d => d.DepartamentoId, (u, d) => new { u, d })
                    .Where(u => u.u.UsuarioId != usuario.UsuarioId)
                    .Select(us => new UsuarioChat
                    {
                        UsuarioId = us.u.UsuarioId,
                        Identificador = us.u.Identificador,
                        Nombre = us.u.Nombre,
                        Apellidos = us.u.Apellidos,
                        FotoPerfil = us.u.FotoPerfil,
                        Email = us.u.Email,
                        Telefono = us.u.Telefono,
                        Telefono2 = us.u.Telefono2,
                        Extension = us.u.Extension,
                        Rol = us.u.Rol,
                        DepartamentoNombre = us.d.Nombre,
                        EstaConectado = us.u.EstaConectado,
                        MensajesSinLeer = 0
                    })
                    .OrderBy(us => us.DepartamentoNombre)
                    .ToArray();

            ICollection<GrupoChat> gruposChat = await _grupoChatRepository.GetGruposChat(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (usuarios.Count > 0 || gruposChat.Count > 0)
            {
                return Ok(new GruposChat() { Usuarios = usuariosChat, Grupos = gruposChat });
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Para conectar el usuario al chat
        /// </summary>
        /// <returns></returns>
        [HttpGet("ConectarUsuario")]
        public async Task<IActionResult> ConectarUsuario()
        {
            var resultado = await _usuarioChatRepository.Conectar(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            var usuario = await _usuarioRepository.GetUsuarioPorId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            await _chatHubContext.Clients.All.SendAsync("ReceiveNotification", usuario.Nombre + " " + usuario.Apellidos + " se ha conectado.");

            if (resultado >= 0)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Para conectar el usuario al chat
        /// </summary>
        /// <returns></returns>
        [HttpGet("DesconectarUsuario")]
        public async Task<IActionResult> DesconectarUsuario()
        {
            var resultado = await _usuarioChatRepository.Desconectar(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            await _chatHubContext.Clients.All.SendAsync("ReceiveNotification", " " + User.FindFirstValue(ClaimTypes.Name) + " se ha desconectado.");

            if (resultado >= 0)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetChats")]
        public async Task<ActionResult<ICollection<MensajeChat>>> GetMensajeChats(Guid grupoChat)
        {
            //TODO: Revisar
            ICollection<MensajeChat> chats  = await _mensajeChatRepository.GetLastMensajesChatPorIdGrupo(grupoChat);

            if (chats.Count >= 0)
            {
                return Ok(chats);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get Historial para chats Peer-to-Peer
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("usuario/{UsuarioId}")]
        public async Task<ActionResult<GrupoChat>> GetHistorialChat(Guid usuarioId)
        {
            Usuario usuario = await _usuarioRepository.GetUsuarioChatPorId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            Usuario receptor = await _usuarioRepository.GetUsuarioChatPorId(usuarioId);

            List<GrupoChatUsuario> usuarios = new()
            {
                new GrupoChatUsuario { Usuario = usuario, UsuarioId = usuario.UsuarioId },
                new GrupoChatUsuario { Usuario = receptor, UsuarioId = usuarioId }
            };

            GrupoChat grupo = new();

            grupo = await _grupoChatRepository.GetGrupoChatPorUsuarios(usuarios);

            if (grupo != null)
            {
                ICollection<MensajeChat> chats = await _mensajeChatRepository.GetLastMensajesChatPorIdGrupo(grupo.GrupoChatId);
                grupo.Mensajes = chats;

                return Ok(grupo);
            }
            else
            {
                grupo = new GrupoChat
                {
                    Usuarios = usuarios,
                    FechaCreado = DateTime.Now,
                    CreadoPor = usuario
                };

                await _grupoChatRepository.Nuevo(grupo);

                return Ok(grupo);
            }
        }

        /// <summary>
        /// Get Historial para chats grupales
        /// </summary>
        /// <param name="grupoId"></param>
        /// <returns></returns>
        [HttpGet("grupo/{grupoId}")]
        public async Task<ActionResult<GrupoChat>> GetHistorialChatGrupal(Guid grupoId)
        {
            GrupoChat grupo = await _grupoChatRepository.GetGrupoChatPorId(grupoId);

            if (grupo != null)
            {
                ICollection<MensajeChat> chats = await _mensajeChatRepository.GetLastMensajesChatPorIdGrupo(grupo.GrupoChatId);
                grupo.Mensajes = chats;

                return Ok(grupo);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Envía chat al Peer-to-Peer
        /// </summary>
        /// <returns></returns>
        [HttpPut("Nuevo")]
        public async Task<ActionResult> EnviarMensaje([FromBody] MensajeChat mensajeChat)
        {
            //TODO: ACABAR
            Usuario usuario = await _usuarioRepository.GetUsuarioChatPorId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            mensajeChat.CreadoPor = usuario;

            ICollection<GrupoChatUsuario> usuariosGrupoChat = await _grupoChatRepository.GetUsuariosGrupoChatPorId(mensajeChat.GrupoChatId);

            foreach (var user in usuariosGrupoChat)
            {
                await _chatHubContext.Clients.User(user.UsuarioId.ToString()).SendAsync("ReceiveMessage", mensajeChat);
            }

            int resultado = await _mensajeChatRepository.Nuevo(mensajeChat, usuario);


            if (resultado > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}