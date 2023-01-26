using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface IJornadasViewModel
	{
		public string Mensaje { get; set; }
		public List<Jornada> Jornadas { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		public Task<HttpResponseMessage> GetJornadasPorUsuario(Guid usuarioId);
		public Task<HttpResponseMessage> GetJornadasPorDepartamento(long departamentoId);
	}
}
