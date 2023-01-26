using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class ZonaRepository : IZonaRepository
    {
        private readonly HelpDeskContext _context;

        public ZonaRepository(HelpDeskContext helpDeskContext)
        {
            this._context = helpDeskContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Zona>> GetZonas()
        {
            return await _context.Zonas.ToListAsync();
        }
    }
}

