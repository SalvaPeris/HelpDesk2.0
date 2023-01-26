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
    public class ZonaViewModel : IZonaViewModel
	{

		public List<Zona> Zonas { get; set; }
		private HttpClient _httpClient;

		public ZonaViewModel()
		{
		}

		public ZonaViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
		/// Devuelve una respuesta con el listado de zonas
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetZonas()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("zona/getzonas");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Zona>>());
			}
			return _response;
		}

		private void CargarObjetoActual(ZonaViewModel zonaViewModel)
		{
			this.Zonas = zonaViewModel.Zonas;
		}

		public static implicit operator ZonaViewModel(List<Zona> zonas)
		{
			return new ZonaViewModel
			{
				Zonas = zonas
			};
		}

		public static implicit operator List<Zona>(ZonaViewModel zonaViewModel)
		{
			return new List<Zona>(zonaViewModel.Zonas);
		}
	}
}


