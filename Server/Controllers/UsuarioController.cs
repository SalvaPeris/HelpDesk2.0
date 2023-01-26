using HelpDesk.Server.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using HelpDesk.Server.Repository;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Hosting;
using MaxMind.GeoIP2.Responses;
using HelpDesk.Server.Services.Mail;
using Microsoft.Extensions.Options;
using HelpDesk.Server.Utils;

namespace HelpDesk.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IOptions<Configuracion> _configuracion;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ILogger<UsuarioController> logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public UsuarioController(IWebHostEnvironment appEnvironment, ILogger<UsuarioController> logger, IOptions<Configuracion> configuracion , HelpDeskContext context)
        {
            this.logger = logger;
            this._configuracion = configuracion;
            this._appEnvironment = appEnvironment;
            this._usuarioRepository = new UsuarioRepository(context);
            this._departamentoRepository = new DepartamentoRepository(context);
        }

#region PERFIL

        /// <summary>
        /// Permite a los usuarios loguearse.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUsuario(Usuario usuario)
        {
            Usuario _usuarioLogueado = await _usuarioRepository.GetUsuarioLogin(usuario.Identificador, usuario.Contrasena);
            Departamento _departamento = await _departamentoRepository.GetDepartamentoPorId(_usuarioLogueado.DepartamentoId);

            if (_usuarioLogueado != null)
            {
                var claimId = new Claim(ClaimTypes.NameIdentifier, _usuarioLogueado.UsuarioId.ToString());
                var claimName = new Claim(ClaimTypes.Name, _usuarioLogueado.Nombre);
                var claimSurname = new Claim(ClaimTypes.Surname, _usuarioLogueado.Apellidos);
                var claimRole = new Claim(ClaimTypes.Role, "Usuario");
                var claimDept = new Claim(ClaimTypes.GroupSid, _departamento.Nombre);

                if (_usuarioLogueado.Rol != null)
                {
                   claimRole = new Claim(ClaimTypes.Role, _usuarioLogueado.Rol);
                }
                
                var claimsIdentity = new ClaimsIdentity(new[] { claimId, claimName, claimSurname, claimRole, claimDept }, "ForumAutenticacion");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Se envía un correo con la contraseña
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        [HttpGet("RecuperarContrasena")]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> RecuperarContrasena(string identificador)
        {
            Usuario _usuario = await _usuarioRepository.GetUsuarioPorDNI(identificador);

            string emailEncriptado = EncryptString.Encrypt( _usuario.Email, '*');

            string contrasenaDesencriptada = Crypto.Crypto.DecryptString(_usuario.Contrasena);

            EmailSender emailSender = new(_configuracion);
            await emailSender.SendEmailAsync(_usuario.Email, "Recuperación de Contraseña", "Su contraseña es " + contrasenaDesencriptada);

            Usuario _usuarioParaEnviar = new()
            {
                Email = emailEncriptado
            };

            if(_usuarioParaEnviar != null)
            {
                return Ok(_usuarioParaEnviar);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza el perfil del usuario actual
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("ActualizarPerfil")]
        public async Task<IActionResult> ActualizarPerfil([FromBody] Usuario usuario)
        {
            Usuario _usuario = await _usuarioRepository.GetUsuarioPorId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            _usuario.SobreMi = usuario.SobreMi;
            _usuario.FotoPerfil = usuario.FotoPerfil;
            _usuario.Telefono = usuario.Telefono;
            _usuario.Telefono2 = usuario.Telefono2;
            _usuario.Extension = usuario.Extension;
            _usuario.Email = usuario.Email;

            string _encryptedPassword = Crypto.Crypto.EncryptString(usuario.Contrasena);
            _usuario.Contrasena = _encryptedPassword;

            var result = await _usuarioRepository.Guardar();

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Consigue el usuario actual
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsuarioActual")]
        [AllowAnonymous] //Debe estar, ya que la primera comprobación para saber si está logueado es esta.
        public async Task<IActionResult> GetUsuarioActual()
        {
            if(User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
            {
                return await GetPerfil();
            }
            else
            {
                return NoContent();
            }

        }

        /// <summary>
        /// Consigue el perfil actual
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPerfil")]
        public async Task<IActionResult> GetPerfil()
        {
            Usuario _usuario = await _usuarioRepository.GetUsuarioPorId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            Departamento departamento = await _departamentoRepository.GetDepartamento(User.FindFirstValue(ClaimTypes.GroupSid));

            string _decryptedPassword = Crypto.Crypto.DecryptString(_usuario.Contrasena);
            _usuario.Contrasena = _decryptedPassword;
            _usuario.DepartamentoNombre = departamento.Nombre;

            if (_usuario != null)
            {
                return Ok(_usuario);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Consigue la ubicación según la ip pública
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLocalizacion")]
        public async Task<ActionResult> GetLocalizacionAsync()
        {
            //var ipAddress = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

            var ipAddress = await Network.GetPublicIPAsync();

            Location location = new Location(_appEnvironment);

            return Ok(location.GetLocationByIp(ipAddress));
        }

        /// <summary>
        /// Cierra la sesión del usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        public async Task<ActionResult<String>> LogOutUsuario()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

 #endregion PERFIL

 #region USUARIOS

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("Nuevo")]
        public async Task<ActionResult<Usuario>> NuevoUsuario([FromBody] Usuario usuario)
        {
            string _encryptedPassword = Crypto.Crypto.EncryptString(usuario.Contrasena);
            usuario.Contrasena = _encryptedPassword;

            var result = await _usuarioRepository.Nuevo(usuario);
            if (result > 0)
            {
                return Ok(usuario);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza el perfil del usuario actual
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            Usuario _usuario = await _usuarioRepository.GetUsuarioPorDNI(usuario.Identificador);

            _usuario.Nombre = usuario.Nombre;
            _usuario.Apellidos = usuario.Apellidos;
            _usuario.SobreMi = usuario.SobreMi;
            _usuario.FotoPerfil = usuario.FotoPerfil;
            _usuario.Telefono = usuario.Telefono;
            _usuario.Telefono2 = usuario.Telefono2;
            _usuario.Extension = usuario.Extension;
            _usuario.Email = usuario.Email;
            _usuario.DepartamentoId = usuario.DepartamentoId;
            _usuario.Rol = usuario.Rol;

            string _encryptedPassword = Crypto.Crypto.EncryptString(usuario.Contrasena);
            _usuario.Contrasena = _encryptedPassword;

            var result = await _usuarioRepository.Guardar();

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza el perfil del usuario actual
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("Eliminar")]
        public async Task<IActionResult> EliminarUsuario([FromBody] Usuario usuario)
        {
            var result = 0;

            if(User.IsInRole("SuperAdministrador") || (User.IsInRole("Administrador") && User.FindFirstValue(ClaimTypes.GroupSid) == "Recursos Humanos"))
            {
                result = await _usuarioRepository.Eliminar(usuario);
            }

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Consigue el usuario por DNI
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsuarioPorDNI")]
        public async Task<IActionResult> GetUsuarioPorDNI(string identificador)
        {
            Usuario usuario = new();

            if (User.IsInRole("SuperAdministrador") || (User.IsInRole("Administrador") && User.FindFirstValue(ClaimTypes.GroupSid) == "Recursos Humanos"))
            {
                usuario = await _usuarioRepository.GetUsuarioPorDNI(identificador);
            }
                
            Departamento departamento = await _departamentoRepository.GetDepartamentoPorId(usuario.DepartamentoId);

            string _decryptedPassword = Crypto.Crypto.DecryptString(usuario.Contrasena);
            usuario.Contrasena = _decryptedPassword;
            usuario.DepartamentoNombre = departamento.Nombre;

            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Consigue el usuario por Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsuarioPorId")]
        public async Task<IActionResult> GetUsuarioPorId(Guid usuarioId)
        {
            Usuario usuario = new();

            if (User.IsInRole("SuperAdministrador") || (User.IsInRole("Administrador") && User.FindFirstValue(ClaimTypes.GroupSid) == "Recursos Humanos"))
            {
                usuario = await _usuarioRepository.GetUsuarioPorId(usuarioId);
            }

            Departamento departamento = await _departamentoRepository.GetDepartamentoPorId(usuario.DepartamentoId);

            string _decryptedPassword = Crypto.Crypto.DecryptString(usuario.Contrasena);
            usuario.Contrasena = _decryptedPassword;
            usuario.DepartamentoNombre = departamento.Nombre;

            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Consigue el nombre y apellidos de un usuario por DNI
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetNombreCompletoPorDNI")]
        public async Task<IActionResult> GetNombreCompletoPorDNI(string identificador)
        {
            Usuario _usuario = new();

            Usuario usuario = await _usuarioRepository.GetUsuarioPorDNI(identificador);

            _usuario.Apellidos = usuario.Apellidos;
            _usuario.Nombre = usuario.Nombre; 

            if (_usuario != null)
            {
                return Ok(_usuario);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Traer los usuarios para la Agenda
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsuarios")]
        public async Task<ActionResult<Usuario[]>> GetUsuarios()
        {
            ICollection<Usuario> usuarios = await _usuarioRepository.GetUsuarios();
            ICollection<Departamento> departamentos = await _departamentoRepository.GetDepartamentos();

            if (usuarios.Count > 0)
            {
                return Ok(usuarios.Join(departamentos, u => u.DepartamentoId, d => d.DepartamentoId, (u, d) => new { u, d })
                                .Select(us => new Usuario
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
                                    DepartamentoNombre = us.d.Nombre
                                })
                                .OrderBy(us => us.DepartamentoNombre)
                                .ToArray());
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Traer los usuarios que coincida con la búsqueda para la Agenda
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpGet("Buscar")]
        public async Task<Usuario[]> BuscarUsuarios(string busqueda)
        {
            ICollection<Usuario> usuarios = await _usuarioRepository.GetUsuarios();
            ICollection<Departamento> departamentos = await _departamentoRepository.GetDepartamentos();

            return usuarios.Join(departamentos, u => u.DepartamentoId, d => d.DepartamentoId, (u, d) => new { u, d })
                .Where(u => new[] { u.u.Nombre, u.u.Apellidos }.Any(u => u.Contains(busqueda)))
                .Select(us => new Usuario
                {
                    Identificador = us.u.Identificador,
                    Nombre = us.u.Nombre,
                    Apellidos = us.u.Apellidos,
                    FotoPerfil = us.u.FotoPerfil,
                    Email = us.u.Email,
                    Telefono = us.u.Telefono,
                    Telefono2 = us.u.Telefono2,
                    Extension = us.u.Extension,
                    Rol = us.u.Rol,
                    DepartamentoNombre = us.d.Nombre
                })
                .OrderBy(us => us.DepartamentoNombre)
                .ToArray();
        }

        /// <summary>
        /// Traer los usuarios que coincida con la búsqueda para la Agenda
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorLetra")]
        public async Task<Usuario[]> BuscarUsuariosPorLetra(string busqueda)
        {
            ICollection<Usuario> usuarios = await _usuarioRepository.GetUsuarios();
            ICollection<Departamento> departamentos = await _departamentoRepository.GetDepartamentos();

            return usuarios.Join(departamentos, u => u.DepartamentoId, d => d.DepartamentoId, (u, d) => new { u, d })
                .Where(u => new[] { u.u.Nombre }.Any(u => u.StartsWith(busqueda)))
                .Select(us => new Usuario
                {
                    Identificador = us.u.Identificador,
                    Nombre = us.u.Nombre,
                    Apellidos = us.u.Apellidos,
                    FotoPerfil = us.u.FotoPerfil,
                    Email = us.u.Email,
                    Telefono = us.u.Telefono,
                    Telefono2 = us.u.Telefono2,
                    Extension = us.u.Extension,
                    Rol = us.u.Rol,
                    DepartamentoNombre = us.d.Nombre
                })
                .OrderBy(us => us.DepartamentoNombre)
                .ToArray();
        }

        #endregion USUARIOS
    }
}
