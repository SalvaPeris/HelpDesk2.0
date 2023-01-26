using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public class TipoTicketViewModel : ITipoTicketViewModel
	{

		public List<TipoTicket> TiposTicket { get; set; }
		private HttpClient _httpClient;

		public TipoTicketViewModel()
		{
		}

		public TipoTicketViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
		/// Devuelve una respuesta con el listado de tipos de Ticket
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTiposTicket()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("tipoticket/gettiposticket");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<TipoTicket>>());
			}
			return _response;
		}

		private void CargarObjetoActual(TipoTicketViewModel tipoTicketViewModel)
		{
			this.TiposTicket = tipoTicketViewModel.TiposTicket;
		}

		public static implicit operator TipoTicketViewModel(List<TipoTicket> tiposTicket)
		{
			return new TipoTicketViewModel
			{
				TiposTicket = tiposTicket
			};
		}

		public static implicit operator List<TipoTicket>(TipoTicketViewModel tipoTicketViewModel)
		{
			return new List<TipoTicket>(tipoTicketViewModel.TiposTicket);
		}
	}
}
