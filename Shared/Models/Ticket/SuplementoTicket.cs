using System;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class SuplementoTicket
	{
		public long SuplementoTicketId { get; set; }
		public string Comentario { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string CreadoPor { get; set; }
		public string CreadoPorNombreCompleto { get; set; }
		public string Imagen { get; set; }
		public long TicketId { get; set; }

		[JsonIgnore]
		public Ticket Ticket { get; set; }
	}
}

