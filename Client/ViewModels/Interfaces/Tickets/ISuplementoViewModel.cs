using System;
using System.ComponentModel.DataAnnotations;
using HelpDesk.Shared.Models;

namespace HelpDesk.ViewModels
{
	public interface ISuplementoViewModel
	{
		public long SuplementoTicketId { get; set; }
		[Required(ErrorMessage = "El comentario es necesario")]
		public string Comentario { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string CreadoPor { get; set; }
		public string CreadoPorNombreCompleto { get; set; }
		public string Imagen { get; set; }
		public Ticket Ticket { get; set; }
		public long TicketId { get; set; }

		public SuplementoTicket GetSingleSuplemento();
		public void SetSingleSuplemento(SuplementoTicket suplementoTicket);
		public void Anular();
	}
}

