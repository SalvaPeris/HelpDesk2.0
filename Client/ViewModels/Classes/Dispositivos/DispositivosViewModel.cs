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
	public class DispositivosViewModel : IDispositivosViewModel
	{
		public string Mensaje { get; set; }
		public List<Dispositivo> Dispositivos { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		private readonly HttpClient _httpClient;

		public DispositivosViewModel()
		{
		}

		public DispositivosViewModel(HttpClient httpClient)
		{
			this._httpClient = httpClient;
		}

		/// <summary>
		/// Trae los dispositivos de un usuario concreto
		/// </summary>
		/// <param name="usuarioId"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetDispositivosPorUsuario(Guid usuarioId)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("dispositivo/getdispositivosporidusuario?idusuario=" + usuarioId);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Dispositivo>>());
			}

			return _response;
		}

		/// <summary>
		/// Trae los dispositivos
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetDispositivos()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("dispositivo/getdispositivos");

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

		public static implicit operator DispositivosViewModel(List<Dispositivo> dispositivos)
		{
			return new DispositivosViewModel
			{
				Dispositivos = dispositivos
			};
		}

		public static implicit operator List<Dispositivo>(DispositivosViewModel dispositivoViewModel)
		{
			return new List<Dispositivo>(dispositivoViewModel.Dispositivos);
		}
	}
}

