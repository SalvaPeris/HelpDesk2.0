using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface IZonaTicketViewModel
	{
        public long ZonaTicketId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Situacion { get; set; }
        public string Observaciones { get; set; }
        public Ticket Ticket { get; set; }
        public long TicketId { get; set; }

		public ZonaTicket GetSingleZonaTicket();
        public void SetSingleZonaTicket(ZonaTicket zonaTicket);

		public void Anular();
	}
}

