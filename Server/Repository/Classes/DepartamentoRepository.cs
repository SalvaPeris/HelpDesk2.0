using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly HelpDeskContext _context;

        public DepartamentoRepository(HelpDeskContext helpDeskContext)
        {
            this._context = helpDeskContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Departamento>> GetDepartamentos()
        {
            return await _context.Departamentos.ToListAsync();
        }

        public async Task<Departamento> GetDepartamentoPorId(long departamentoId)
        {
            return await _context.Departamentos.Where(d => d.DepartamentoId == departamentoId).FirstOrDefaultAsync();
        }

        public async Task<Departamento> GetDepartamento(string departamento)
        {
            return await _context.Departamentos.Where(d => d.Nombre == departamento).FirstOrDefaultAsync();
        }
    }
}

