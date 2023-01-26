using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public class TicketViewModel : ITicketViewModel
	{

		public string Mensaje { get; set; }
		public List<Ticket> Tickets { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }
		private HttpClient _httpClient;

		public TicketViewModel()
		{
		}

		public TicketViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
		/// Consigue el número total de los tickets.
		/// </summary>
		/// <returns></returns>
		public async Task<int> CargarTotalTickets(string Estado)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/totaltickets?estado=" + Estado);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				return await _response.Content.ReadFromJsonAsync<int>();
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
        /// Consigue el número total de tickets propios.
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
		public async Task<int> CargarTotalTicketsPropios(string Estado)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/totalticketspropios?estado=" + Estado);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				return await _response.Content.ReadFromJsonAsync<int>();
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
        /// Consigue el número total de tickets asignados.
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
		public async Task<int> CargarTotalTicketsAsignados(string Estado)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/totalticketsasignados?estado=" + Estado);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				return await _response.Content.ReadFromJsonAsync<int>();
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Consigue los tickets con la paginación.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> CargarTickets(int skip, int top)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/tickets?skip=" + skip + "&top=" + top);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Ticket>>());
			}
			return _response;
		}

		/// <summary>
		/// Consigue los tickets según el estado que le pases.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTickets(string estado, int skip, int top)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/" + estado + "?skip=" + skip + "&top=" + top);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
                CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Ticket>>());
			}
			return _response;
		}


		/// <summary>
		/// Consigue los tickets según el estado que le pases.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsPropios(string estado, int skip, int top)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/mistickets?estado=" + estado + "&skip=" + skip + "&top=" + top);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Ticket>>());
			}
			return _response;
		}

		/// <summary>
		/// Consigue los tickets según el estado que le pases.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsAsignados(string estado, int skip, int top)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/ticketsasignados?estado=" + estado + "&skip=" + skip + "&top=" + top);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Ticket>>());
			}
			return _response;
		}

		/// <summary>
		/// Consigue los tickets activos.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsActivos(int skip, int top)
		{
			//TODO: Si en un futuro hay mucha carga en el servidor, no haremos peticiones al filtrar
			//List<Ticket> _tickets = this.Tickets.FindAll(t => t.Estado == "Activo");
			return await GetTickets("Activos", skip, top);
		}

		/// <summary>
		/// Consigue los tickets propios activos.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsPropiosActivos(int skip, int top)
		{
			//TODO: Si en un futuro hay mucha carga en el servidor, no haremos peticiones al filtrar
			//List<Ticket> _tickets = this.Tickets.FindAll(t => t.Estado == "Activo");
			return await GetTicketsPropios("Activo", skip, top);
		}

		/// <summary>
		/// Consigue los tickets propios activos.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsAsignadosActivos(int skip, int top)
		{
			//TODO: Si en un futuro hay mucha carga en el servidor, no haremos peticiones al filtrar
			//List<Ticket> _tickets = this.Tickets.FindAll(t => t.Estado == "Activo");
			return await GetTicketsAsignados("Activo", skip, top);
		}

		/// <summary>
		/// Consigue los tickets vistos.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsVistos(int skip, int top)
		{
			return await GetTickets("Vistos", skip, top);
		}

		/// <summary>
		/// Consigue los tickets propios vistos.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsPropiosVistos(int skip, int top)
		{
			return await GetTicketsPropios("Visto", skip, top);
		}

		/// <summary>
		/// Consigue los tickets asignados vistos.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsAsignadosVistos(int skip, int top)
		{
			return await GetTicketsAsignados("Visto", skip, top);
		}

		/// <summary>
		/// Consigue los tickets pendientes.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsPendientes(int skip, int top)
		{
			return await GetTickets("Pendientes", skip, top);
		}

		/// <summary>
		/// Consigue los tickets propios pendientes.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsPropiosPendientes(int skip, int top)
		{
			return await GetTicketsPropios("Pendiente", skip, top);
		}

		/// <summary>
		/// Consigue los tickets asignados pendientes.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsAsignadosPendientes(int skip, int top)
		{
			return await GetTicketsAsignados("Pendiente", skip, top);
		}

		/// <summary>
		/// Consigue los tickets solucionados.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsSolucionados(int skip, int top)
		{
			return await GetTickets("Solucionados", skip, top);
		}

		/// <summary>
		/// Consigue los tickets propios solucionados.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsPropiosSolucionados(int skip, int top)
		{
			return await GetTicketsPropios("Solucionado", skip, top);
		}

		/// <summary>
		/// Consigue los tickets propios solucionados.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsAsignadosSolucionados(int skip, int top)
		{
			return await GetTicketsAsignados("Solucionado", skip, top);
		}

		/// <summary>
		/// Consigue los tickets.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsTodos(int skip, int top)
		{
			return await GetTickets("Todos", skip, top);
		}

		/// <summary>
		/// Consigue los tickets propios.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsPropiosTodos(int skip, int top)
		{
			return await GetTicketsPropios("Todos", skip, top);
		}

		/// <summary>
		/// Consigue los tickets asignados.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetTicketsAsignadosTodos(int skip, int top)
		{
			return await GetTicketsAsignados("Todos", skip, top);
		}

		/// <summary>
		/// Consigue los tickets departamentales según cadena de búsqueda
		/// </summary>
		/// <param name="busqueda"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> BuscarTicketsDepartamentales(string busqueda)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/buscardepartamentales?busqueda=" + busqueda);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Ticket>>());
			}
			return _response;
		}

		/// <summary>
		/// Consigue los tickets propios según cadena de búsqueda
		/// </summary>
		/// <param name="busqueda"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> BuscarTicketsPropios(string busqueda)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/buscarpropios?busqueda=" + busqueda);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Ticket>>());
			}
			return _response;
		}

		/// <summary>
		/// Consigue los tickets asignados según cadena de búsqueda
		/// </summary>
		/// <param name="busqueda"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> BuscarTicketsAsignados(string busqueda)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/buscarasignados?busqueda=" + busqueda);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Ticket>>());
			}
			return _response;
		}

		/// <summary>
		/// Consigue los tickets según el estado que le pases.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> CambioEstadoTicket(string estado, long ticketId)
		{
			return await _httpClient.PutAsJsonAsync("ticket/" + estado, ticketId);
		}

		/// <summary>
		/// Cambiar estado a solucionado.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> SolucionarTicket(long ticketId)
		{
			return await CambioEstadoTicket("solucionar", ticketId);
		}

		/// <summary>
		/// Cambiar estado a pendiente.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> PendienteTicket(long ticketId)
		{
			return await CambioEstadoTicket("pendiente", ticketId);
		}

		/// <summary>
		/// Cambiar estado a activo.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> ActivarTicket(long ticketId)
		{
			return await CambioEstadoTicket("activar", ticketId);
		}

		/// <summary>
		/// Cambiar estado a visto.
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> VistoTicket(long ticketId)
		{
			return await CambioEstadoTicket("visto", ticketId);
		}

		private void CargarObjetoActual(TicketViewModel ticketViewModel)
		{
			this.Tickets = ticketViewModel.Tickets;
		}

        public static implicit operator TicketViewModel(List<Ticket> tickets)
		{
			return new TicketViewModel
			{
				Tickets = tickets
			};
		}

		public static implicit operator List<Ticket>(TicketViewModel ticketViewModel)
		{
			return new List<Ticket>(ticketViewModel.Tickets);
		}
	}
}

