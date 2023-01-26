using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IDepartamentoRepository : IDisposable
	{
		Task<ICollection<Departamento>> GetDepartamentos();

		Task<Departamento> GetDepartamento(string departamento);
		Task<Departamento> GetDepartamentoPorId(long departamentoId);
	}
}

