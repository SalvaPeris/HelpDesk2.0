using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class EmpresaExternaRepository : IEmpresaExternaRepository
    {
        private readonly HelpDeskContext _context;

        public EmpresaExternaRepository(HelpDeskContext helpDeskContext)
        {
            this._context = helpDeskContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<EmpresaExterna>> GetEmpresasExternas()
        {
            return await _context.EmpresasExternas.ToListAsync();
        }
    }
}