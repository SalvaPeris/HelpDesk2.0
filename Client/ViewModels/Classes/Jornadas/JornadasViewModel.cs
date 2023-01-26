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
	public class JornadasViewModel : IJornadasViewModel
	{
		public string Mensaje { get; set; }
		public List<Jornada> Jornadas { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		private readonly HttpClient _httpClient;

		public JornadasViewModel()
		{
		}

		public JornadasViewModel(HttpClient httpClient)
		{
			this._httpClient = httpClient;
		}

		/// <summary>
		/// Trae los fichajes de un usuario concreto
		/// </summary>
		/// <param name="usuarioId"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetJornadasPorUsuario(Guid usuarioId)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("jornada/getfichajesporidusuario?idusuario=" + usuarioId);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Jornada>>());
			}

			return _response;
		}

		/// <summary>
		/// Trae los fichajes de un departamento concreto
		/// </summary>
		/// <param name="departamentoId"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetJornadasPorDepartamento(long departamentoId)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("jornada/getfichajespordepartamento");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Jornada>>());
			}

			return _response;
		}

		private void CargarObjetoActual(JornadasViewModel jornadasViewModel)
		{
			this.Jornadas = jornadasViewModel.Jornadas;
		}

		public static implicit operator JornadasViewModel(List<Jornada> jornadas)
		{
			return new JornadasViewModel
			{
				Jornadas = jornadas
			};
		}

		public static implicit operator List<Jornada>(JornadasViewModel jornadasViewModel)
		{
			return new List<Jornada>(jornadasViewModel.Jornadas);
		}
	}
}

