using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class GrupoChatRepository : IGrupoChatRepository
    {
        private readonly HelpDeskContext _context;

        public GrupoChatRepository(HelpDeskContext helpdeskContext)
        {
            this._context = helpdeskContext;
        }

        public void Dispose()
        {
            //No hace falta, DBContext ya maneja las conexiones.
            throw new NotImplementedException();
        }

        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Nuevo(GrupoChat grupoChat)
        {
            _context.GruposChat.Add(grupoChat);
            return await Guardar();
        }


        public async Task<int> Actualizar(GrupoChat grupoChat)
        {
            _context.GruposChat.Update(grupoChat);
            return await Guardar();
        }


        public async Task<int> Eliminar(GrupoChat grupoChat)
        {
            _context.GruposChat.Remove(grupoChat);
            return await Guardar();
        }


        public async Task<ICollection<GrupoChatUsuario>> GetUsuariosGrupoChatPorId(Guid grupoId)
        {
            return await _context.GruposChat
                .Where(g => g.GrupoChatId == grupoId)
                .SelectMany(g => g.Usuarios).ToListAsync();
        }


        public async Task<GrupoChat> GetGrupoChatPorUsuarios(ICollection<GrupoChatUsuario> usuarios)
        {
            return await _context.GruposChat
                .Where(g => g.Usuarios.Count == 2)
                .Where(g => g.Usuarios.Any(u => u.UsuarioId == usuarios.First().Usuario.UsuarioId))
                .Where(g => g.Usuarios.Any(u => u.UsuarioId == usuarios.Last().Usuario.UsuarioId))
                .FirstOrDefaultAsync();
        }


        public async Task<ICollection<GrupoChat>> GetGruposChat(Guid usuarioId)
        {
            return await _context.GruposChat
                .Where(g => g.Usuarios.Count > 2)
                .Where(d => d.Usuarios.Any(u => u.UsuarioId == usuarioId))
                .Select(g => new GrupoChat()
                {
                    GrupoChatId = g.GrupoChatId,
                    Titulo = g.Titulo,
                    CreadoPor = g.CreadoPor,
                    CuentaUsuarios = g.Usuarios.Count(),
                    MensajesSinLeer = g.Mensajes.Count(m => m.Estados.Any(m => m.UsuarioId == usuarioId && m.Leido == false))
                })
                .OrderBy(g => g.GrupoChatId)
                .ToListAsync();
            //.Include(g => g.Mensajes.Any(m => m.Estados.Any(m => m.UsuarioId == usuarioId && m.Leido == false)))
            //.ToDictionaryAsync(d => d);
        }

        public async Task<GrupoChat> GetGrupoChatPorId(Guid grupoId)
        {
            return await _context.GruposChat
                .Where(g => g.GrupoChatId == grupoId)
                .FirstOrDefaultAsync();
        }
    }
}
