using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface ISingleTicketViewModel
	{
		public string Mensaje { get; set; }

		public long TicketId { get; set; }

		[Required(ErrorMessage = "El título es necesario")]
		public string Titulo { get; set; }
		public bool Publico { get; set; }
		[Required(ErrorMessage = "La área del problema es necesaria")]
		public string Area { get; set; }
		public string TipoTicket { get; set; }
		[Required(ErrorMessage = "La descripción del problema es necesaria")]
		public string Observaciones { get; set; }
		public string RemotoApp { get; set; }
		public string RemotoID { get; set; }
		public string RemotoContrasena { get; set; }
		public string Imagen { get; set; }
		public string Gravedad { get; set; }
		public string Estado { get; set; }
		public string CreadoPor { get; set; }
		public string CreadoPorNombreCompleto { get; set; }
		public string AsignadoA { get; set; }
		public string AsignadoANombreCompleto { get; set; }
		[Required(ErrorMessage = "El teléfono es necesario")]
		public string TelefonoContacto { get; set; }
		public string EmailContacto { get; set; }
		public DateTime FechaCreado { get; set; }
		public string FechaSolucionado { get; set; }
		public string SolucionadoPor { get; set; }
		public string SolucionadoPorNombreCompleto { get; set; }
		public ICollection<SuplementoTicket> Suplementos { get; set; }
		public ICollection<EstadoTicket> Estados { get; set; }
		public ICollection<ZonaTicket> Zonas { get; set; }

		public NotificationSeverity NotificacionSeveridad { get; set; }

		public Task<HttpResponseMessage> GetTicket(long ticketId);
		public Ticket GetSingleTicket();
		public Task<HttpResponseMessage> NuevoTicket();
		public Task<HttpResponseMessage> ActualizarTicket();
		public Task<HttpResponseMessage> ActualizarYSolucionarTicket();
		public Task<HttpResponseMessage> EliminarTicket();
		public void CargarObjetoActual(SingleTicketViewModel singleTicketViewModel);
		public void Anular();

		public void NuevoSuplemento(SuplementoTicket suplementoTicket);
		public void ModificarSuplemento(SuplementoTicket suplementoTicket, SuplementoTicket suplementoTicketModificado);

		public void NuevoEstado(EstadoTicket estadoTicket);

		public void NuevaZonaTicket(ZonaTicket zonaTicket);
		public void ModificarZonaTicket(ZonaTicket zonaTicket, ZonaTicket zonaTicketModificado);

		public Task<HttpResponseMessage> SetNombreCompletoUsuarioPorDNICreadoPor(string identificador);
		public Task<HttpResponseMessage> SetNombreCompletoUsuarioPorDNIAsignadoA(string identificador);
	}
}


