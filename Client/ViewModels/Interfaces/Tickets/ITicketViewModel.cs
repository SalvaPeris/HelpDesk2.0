using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface ITicketViewModel
	{
		public string Mensaje { get; set; }
		public List<Ticket> Tickets { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		public Task<HttpResponseMessage> CargarTickets(int skip, int top);
		public Task<int> CargarTotalTickets(string Estado);
		public Task<int> CargarTotalTicketsPropios(string Estado);
		public Task<int> CargarTotalTicketsAsignados(string Estado);

		public Task<HttpResponseMessage> GetTicketsActivos(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsPendientes(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsSolucionados(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsVistos(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsTodos(int skip, int top);

		public Task<HttpResponseMessage> GetTicketsPropiosActivos(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsPropiosVistos(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsPropiosPendientes(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsPropiosSolucionados(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsPropiosTodos(int skip, int top);

		public Task<HttpResponseMessage> GetTicketsAsignadosActivos(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsAsignadosVistos(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsAsignadosPendientes(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsAsignadosSolucionados(int skip, int top);
		public Task<HttpResponseMessage> GetTicketsAsignadosTodos(int skip, int top);

		public Task<HttpResponseMessage> BuscarTicketsDepartamentales(string busqueda);
		public Task<HttpResponseMessage> BuscarTicketsPropios(string busqueda);
		public Task<HttpResponseMessage> BuscarTicketsAsignados(string busqueda);

		public Task<HttpResponseMessage> SolucionarTicket(long ticketId);
		public Task<HttpResponseMessage> PendienteTicket(long ticketId);
		public Task<HttpResponseMessage> ActivarTicket(long ticketId);
		public Task<HttpResponseMessage> VistoTicket(long ticketId);
	}
}

