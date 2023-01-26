using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public interface ISettingsViewModel
    {
        public bool Notificaciones { get; set; }
        public bool TemaOscuro { get; set; }

        public int EstadoTickets { get; set; }

        public string EstadoMenuTickets { get; set; }
        public int EstadoCuentaTickets { get; set; }
        public DateTime EstadoUltimaActualizacionTickets { get; set; }

        public string EstadoNavegacion { get; set; }

        public Localizacion Localizacion { get; set; }

        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }

        public Task GetPerfil();
        public Task<HttpResponseMessage> GetLocalizacion();

        public string GetEstadoMenuTickets();
        public int GetEstadoTickets();

        public int GetEstadoCuentaTickets();

    }
}
