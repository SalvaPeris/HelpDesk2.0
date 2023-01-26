using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IZonaRepository : IDisposable
	{
		Task<ICollection<Zona>> GetZonas();
	}
}

