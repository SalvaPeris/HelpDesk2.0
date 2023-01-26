using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
namespace HelpDesk.ViewModels
{
	public interface ITipoDispositivoViewModel
	{
		public List<TipoDispositivo> TiposDispositivo { get; set; }

		public Task<HttpResponseMessage> GetTiposDispositivo();
	}
}