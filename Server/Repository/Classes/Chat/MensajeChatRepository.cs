using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class MensajeChatRepository : IMensajeChatRepository
    {
        private readonly HelpDeskContext _context;

        public MensajeChatRepository(HelpDeskContext helpdeskContext)
        {
            this._context = helpdeskContext;
        }

        public void Dispose()
        {
            //No hace falta, DBContext ya maneja las conexiones.
            throw new NotImplementedException();
        }

        public async Task<ICollection<MensajeChat>> GetLastMensajesChatPorIdGrupo(Guid grupoId)
        {
            return await _context.Chats
                .Where(c => c.GrupoChatId == grupoId)
                .Include(c => c.CreadoPor)
                .Include(c => c.Estados)
                .OrderByDescending(c => c.FechaCreado)
                .Take(20)
                .Reverse()
                .ToListAsync();
        }

        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Nuevo(MensajeChat mensajeChat, Usuario usuario)
        {
            mensajeChat.CreadoPor = usuario;
            mensajeChat.FechaCreado = DateTime.Now;

            _context.Chats.Add(mensajeChat);

            return await Guardar();
        }

        public async Task<int> Actualizar(MensajeChat mensajeChat)
        {
            _context.Chats.Update(mensajeChat);
            return await Guardar();
        }

        public async Task<int> Eliminar(MensajeChat mensajeChat)
        {
            _context.Chats.Remove(mensajeChat);
            return await Guardar();
        }
    }
}

