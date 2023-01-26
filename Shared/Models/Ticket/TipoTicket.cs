using System;
using System.Collections.Generic;

namespace HelpDesk.Shared.Models
{
	public class TipoTicket
	{
		public long TipoTicketId { get; set; }
		public string Nombre { get; set; }
		public string Observaciones { get; set; }
	}
}