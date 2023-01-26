using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public class JornadaViewModel : IJornadaViewModel
	{
		public string Mensaje { get; set; }
		public Jornada JornadaActual { get; set; }
		public List<Jornada> Jornadas { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		private readonly HttpClient _httpClient;

		public JornadaViewModel()
		{
		}

		public JornadaViewModel(HttpClient httpClient)
		{
			this._httpClient = httpClient;
		}

		/// <summary>
		/// Trae los fichajes del usuario actual
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetMisFichajes(DateTime FechaComienzo, DateTime FechaFin)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("jornada/misfichajes?fechacomienzo=" + FechaComienzo.ToString("yyyy-MM-ddTHH:mm:ss") + "&fechafin=" + FechaFin.ToString("yyyy-MM-ddTHH:mm:ss") );
			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Jornada>>());
				if (this.JornadaActual == null)
                {
					this.JornadaActual = Jornadas.Where(j => j.FechaJornada.DayOfYear == DateTime.Now.DayOfYear).FirstOrDefault();

					if(this.JornadaActual == null)
						this.JornadaActual = new Jornada
						{
							FechaJornada = DateTime.Today,
							Fichajes = new List<Fichaje>(),
							Ausencias = new List<JornadaAusencia>(),
							Turnos = new List<JornadaTurno>()
						};
                }
			}
			return _response;
		}

		/// <summary>
		/// Crea un nuevo Fichaje
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> NuevoFichaje()
		{
			HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("jornada/abrirfichaje", this.JornadaActual);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				JornadaActual = await _response.Content.ReadFromJsonAsync<Jornada>();

				Jornada JornadaAModificar = Jornadas.Where(j => j.FechaJornada.DayOfYear == DateTime.Now.DayOfYear).FirstOrDefault();
				if(JornadaAModificar != null)
                {
					JornadaAModificar.Fichajes = JornadaActual.Fichajes;
                }
			}
			return _response;
		}

		/// <summary>
		/// Finaliza un Fichaje
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> FinalizaFichaje()
		{
			HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("jornada/finalizafichaje", this.JornadaActual);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				JornadaActual = await _response.Content.ReadFromJsonAsync<Jornada>();

				Jornada JornadaAModificar = Jornadas.Where(j => j.FechaJornada.DayOfYear == DateTime.Now.DayOfYear).FirstOrDefault();
				if (JornadaAModificar != null)
				{
					JornadaAModificar.Fichajes = JornadaActual.Fichajes;
				}
			}
			return _response;
		}

		private void CargarObjetoActual(JornadaViewModel jornadaViewModel)
		{
			this.Jornadas = jornadaViewModel;
		}

		public static implicit operator JornadaViewModel(List<Jornada> jornadas)
		{
			return new JornadaViewModel
			{
				Jornadas = jornadas
			};
		}

		public static implicit operator List<Jornada>(JornadaViewModel jornadaViewModel)
		{
			return new List<Jornada>(jornadaViewModel.Jornadas);
		}
	}
}

