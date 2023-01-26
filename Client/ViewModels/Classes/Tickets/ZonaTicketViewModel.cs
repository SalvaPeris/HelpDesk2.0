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
    public class ZonaTicketViewModel : IZonaTicketViewModel
    {
        public string Mensaje { get; set; }
        public long ZonaTicketId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Situacion { get; set; }
        public string Observaciones { get; set; }
        public Ticket Ticket { get; set; }
        public long TicketId { get; set; }

        public NotificationSeverity NotificacionSeveridad { get; set; }
        private HttpClient _httpClient;

        public ZonaTicketViewModel()
        {
        }

        public ZonaTicketViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Anular()
        {
            this.Nombre = null;
            this.Situacion = null;
            this.Observaciones = null;
            this.Ticket = null;
            this.TicketId = 0;
        }

        public ZonaTicket GetSingleZonaTicket()
        {
            ZonaTicket zonaTicket = new();
            zonaTicket.ZonaTicketId = this.ZonaTicketId;
            zonaTicket.Nombre = this.Nombre;
            zonaTicket.Situacion = this.Situacion;
            zonaTicket.Observaciones = this.Observaciones;
            zonaTicket.Ticket = this.Ticket;
            zonaTicket.TicketId = this.TicketId;

            return zonaTicket;
        }

        public void SetSingleZonaTicket(ZonaTicket zonaTicket)
        {
            this.ZonaTicketId = zonaTicket.ZonaTicketId;
            this.Nombre = zonaTicket.Nombre;
            this.Situacion = zonaTicket.Situacion;
            this.Observaciones = zonaTicket.Observaciones;
            this.Ticket = zonaTicket.Ticket;
            this.TicketId = zonaTicket.TicketId;
        }

        public static implicit operator ZonaTicketViewModel(ZonaTicket zonaTicket)
        {
            return new ZonaTicketViewModel
            {
                ZonaTicketId = zonaTicket.ZonaTicketId,
                Nombre = zonaTicket.Nombre,
                Situacion = zonaTicket.Situacion,
                Observaciones = zonaTicket.Observaciones,
                Ticket = zonaTicket.Ticket,
                TicketId = zonaTicket.TicketId
            };
        }

        public static implicit operator ZonaTicket(ZonaTicketViewModel zonaTicketViewModel)
        {
            return new ZonaTicket
            {
                ZonaTicketId = zonaTicketViewModel.ZonaTicketId,
                Nombre = zonaTicketViewModel.Nombre,
                Situacion = zonaTicketViewModel.Situacion,
                Observaciones = zonaTicketViewModel.Observaciones,
                Ticket = zonaTicketViewModel.Ticket,
                TicketId = zonaTicketViewModel.TicketId
            };
        }
    }
}