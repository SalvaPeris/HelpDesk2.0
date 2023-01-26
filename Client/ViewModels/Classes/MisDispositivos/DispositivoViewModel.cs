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
	public class DispositivoViewModel : IDispositivoViewModel
	{
		public string Mensaje { get; set; }
		public List<Dispositivo> Dispositivos { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		private readonly HttpClient _httpClient;

		public DispositivoViewModel()
		{
		}

		public DispositivoViewModel(HttpClient httpClient)
		{
			this._httpClient = httpClient;
		}

		/// <summary>
        /// Trae los dispositivos del usuario actual
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> GetMisDispositivos()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("dispositivo/misdispositivos");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Dispositivo>>());
			}

			return _response;
		}

		private void CargarObjetoActual(DispositivoViewModel dispositivoViewModel)
        {
			this.Dispositivos = dispositivoViewModel;
		}

		public static implicit operator DispositivoViewModel(List<Dispositivo> dispositivos)
		{
			return new DispositivoViewModel
			{
				Dispositivos = dispositivos
			};
		}

		public static implicit operator List<Dispositivo>(DispositivoViewModel dispositivoViewModel)
		{
			return new List<Dispositivo>(dispositivoViewModel.Dispositivos);
		}
	}
}

