using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class UsuarioChatRepository : IUsuarioChatRepository
    {
        private readonly HelpDeskContext _context;

        public UsuarioChatRepository(HelpDeskContext helpDeskContext)
        {
            this._context = helpDeskContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Conectar(Guid usuarioId)
        {
            Usuario usuario = await _context.Usuarios.Where(u => u.UsuarioId == usuarioId).FirstOrDefaultAsync();
            usuario.EstaConectado = true;
            return await Guardar();
        }

        public async Task<int> Desconectar(Guid usuarioId)
        {
            Usuario usuario = await _context.Usuarios.Where(u => u.UsuarioId == usuarioId).FirstOrDefaultAsync();
            usuario.EstaConectado = false;
            return await Guardar();
        }
    }
}