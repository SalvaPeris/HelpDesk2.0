using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public class SuplementoViewModel : ISuplementoViewModel
	{

		public string Mensaje { get; set; }
		public long SuplementoTicketId { get; set; }
		[Required(ErrorMessage = "El comentario es necesario")]
		public string Comentario { get; set; }
		public DateTime FechaCreacion { get; set; }
		public string CreadoPor { get; set; }
		public string CreadoPorNombreCompleto { get; set; }
		public string Imagen { get; set; }
		public Ticket Ticket { get; set; }
		public long TicketId { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }
		private HttpClient _httpClient;

		public SuplementoViewModel()
		{
		}

		public SuplementoViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public SuplementoTicket GetSingleSuplemento()
		{
			SuplementoTicket suplementoTicket = new();
			suplementoTicket.SuplementoTicketId = this.SuplementoTicketId;
			suplementoTicket.Comentario = this.Comentario;
			suplementoTicket.FechaCreacion = this.FechaCreacion;
			suplementoTicket.CreadoPor = this.CreadoPor;
			suplementoTicket.CreadoPorNombreCompleto = this.CreadoPorNombreCompleto;
			suplementoTicket.Imagen = this.Imagen;
			suplementoTicket.Ticket = this.Ticket;
			suplementoTicket.TicketId = this.TicketId;

			return suplementoTicket;
		}

		public void SetSingleSuplemento(SuplementoTicket suplementoTicket)
		{
			this.SuplementoTicketId = suplementoTicket.SuplementoTicketId;
			this.Comentario = suplementoTicket.Comentario ;
			this.FechaCreacion = suplementoTicket.FechaCreacion;
			this.CreadoPor = suplementoTicket.CreadoPor;
			this.CreadoPorNombreCompleto = suplementoTicket.CreadoPorNombreCompleto;
			this.Imagen = suplementoTicket.Imagen;
			this.Ticket = suplementoTicket.Ticket;
			this.TicketId = suplementoTicket.TicketId;
		}

		public void Anular()
		{
			this.SuplementoTicketId = 0;
			this.Comentario = null;
			this.FechaCreacion = DateTime.Now;
			this.CreadoPor = null;
			this.CreadoPorNombreCompleto = null;
			this.Imagen = null;
			this.Ticket = null;
			this.TicketId = 0;
		}

		public static implicit operator SuplementoViewModel(SuplementoTicket suplementoTicket)
		{
			return new SuplementoViewModel
			{
				SuplementoTicketId = suplementoTicket.SuplementoTicketId,
				Comentario = suplementoTicket.Comentario,
				FechaCreacion = suplementoTicket.FechaCreacion,
				CreadoPor = suplementoTicket.CreadoPor,
				CreadoPorNombreCompleto = suplementoTicket.CreadoPorNombreCompleto,
				Imagen = suplementoTicket.Imagen,
				Ticket = suplementoTicket.Ticket,
				TicketId = suplementoTicket.TicketId
			};
		}

		public static implicit operator SuplementoTicket(SuplementoViewModel suplementoViewModel)
		{
			return new SuplementoTicket
			{
				SuplementoTicketId = suplementoViewModel.SuplementoTicketId,
				Comentario = suplementoViewModel.Comentario,
				FechaCreacion = suplementoViewModel.FechaCreacion,
				CreadoPor = suplementoViewModel.CreadoPor,
				CreadoPorNombreCompleto = suplementoViewModel.CreadoPorNombreCompleto,
				Imagen = suplementoViewModel.Imagen,
				Ticket = suplementoViewModel.Ticket,
				TicketId = suplementoViewModel.TicketId
			};
		}
	}
}