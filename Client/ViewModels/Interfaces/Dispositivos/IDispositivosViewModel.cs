using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface IDispositivosViewModel
	{
		public string Mensaje { get; set; }
		public List<Dispositivo> Dispositivos { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		public Task<HttpResponseMessage> GetDispositivosPorUsuario(Guid usuarioId);
		public Task<HttpResponseMessage> GetDispositivos();
	}
}
