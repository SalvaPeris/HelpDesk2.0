using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface IEstadoTicketViewModel
	{
		public long EstadoTicketId { get; set; }
		public string Estado { get; set; }
		public string EstadoPor { get; set; }
		public string EstadoPorNombreCompleto { get; set; }
		public DateTime FechaEstado { get; set; }
		public string Observaciones { get; set; }
		public Ticket Ticket { get; set; }
		public long TicketId { get; set; }

		public Task<HttpResponseMessage> GetEstados(long ticketId);
		public Task<HttpResponseMessage> NuevoEstado();
		public Task EliminarEstado(SuplementoTicket suplementoTicket);
		public void Anular();
	}
}