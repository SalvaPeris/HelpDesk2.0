using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.ViewModels
{
	public class TipoDispositivoViewModel : ITipoDispositivoViewModel
	{

		public List<TipoDispositivo> TiposDispositivo { get; set; }
		private HttpClient _httpClient;

		public TipoDispositivoViewModel()
		{
		}

		public TipoDispositivoViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
		/// Devuelve una respuesta con el listado de tipos de Ticket
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTiposDispositivo()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("tipodispositivo/gettipos");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<TipoDispositivo>>());
			}
			return _response;
		}

		private void CargarObjetoActual(TipoDispositivoViewModel tipoDispositivoViewModel)
		{
			this.TiposDispositivo = tipoDispositivoViewModel.TiposDispositivo;
		}

		public static implicit operator TipoDispositivoViewModel(List<TipoDispositivo> tiposDispositivo)
		{
			return new TipoDispositivoViewModel
			{
				TiposDispositivo = tiposDispositivo
			};
		}

		public static implicit operator List<TipoDispositivo>(TipoDispositivoViewModel tipoDispositivoViewModel)
		{
			return new List<TipoDispositivo>(tipoDispositivoViewModel.TiposDispositivo);
		}
	}
}