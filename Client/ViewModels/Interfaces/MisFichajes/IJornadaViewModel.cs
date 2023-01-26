using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface IJornadaViewModel
	{
		public string Mensaje { get; set; }
		public Jornada JornadaActual { get; set; }
		public List<Jornada> Jornadas { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		public Task<HttpResponseMessage> GetMisFichajes(DateTime FechaComienzo, DateTime FechaFin);
		public Task<HttpResponseMessage> NuevoFichaje();
		public Task<HttpResponseMessage> FinalizaFichaje();

	}
}
