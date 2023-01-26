using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class TipoTicketRepository : ITipoTicketRepository
    {
        private readonly HelpDeskContext _context;

        public TipoTicketRepository(HelpDeskContext helpDeskContext)
        {
            this._context = helpDeskContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<TipoTicket>> GetTipoTickets()
        {
            return _context.TiposTicket.ToListAsync();
        }
    }
}