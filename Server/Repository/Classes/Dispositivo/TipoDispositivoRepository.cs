using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class TipoDispositivoRepository : ITipoDispositivoRepository
    {
        private readonly HelpDeskContext _context;

        public TipoDispositivoRepository(HelpDeskContext helpDeskContext)
        {
            this._context = helpDeskContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<TipoDispositivo>> GetTiposDispositivo()
        {
            return await _context.TiposDispositivo.ToListAsync();
        }
    }
}