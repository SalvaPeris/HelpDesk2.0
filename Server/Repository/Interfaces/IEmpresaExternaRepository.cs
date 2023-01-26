using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IEmpresaExternaRepository : IDisposable
	{
		Task<ICollection<EmpresaExterna>> GetEmpresasExternas();
	}
}
