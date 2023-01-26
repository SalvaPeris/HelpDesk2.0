using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class EstadoTicket
	{
		public long EstadoTicketId { get; set; }
		public string Estado { get; set; }
		public string EstadoPor { get; set; }
		public string EstadoPorNombreCompleto { get; set; }
		public DateTime FechaEstado { get; set; }
		public string Observaciones { get; set; }
		public long TicketId { get; set; }
		[JsonIgnore]
		public Ticket Ticket { get; set; }
	}
}
