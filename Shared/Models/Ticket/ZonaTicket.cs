using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class ZonaTicket
	{
		public long ZonaTicketId { get; set; }
		public string Nombre { get; set; }
		public string Situacion { get; set; }
		public string Observaciones { get; set; }
		public long TicketId { get; set; }
		[JsonIgnore]
		public Ticket Ticket { get; set; }
	}
}