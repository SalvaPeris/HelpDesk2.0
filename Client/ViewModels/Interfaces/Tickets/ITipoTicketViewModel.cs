using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
namespace HelpDesk.ViewModels
{
	public interface ITipoTicketViewModel
	{
		public List<TipoTicket> TiposTicket { get; set; }

		public Task<HttpResponseMessage> GetTiposTicket();
	}
}