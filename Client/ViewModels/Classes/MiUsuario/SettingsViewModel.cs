using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using MaxMind.GeoIP2.Responses;
using Radzen;

namespace HelpDesk.ViewModels
{
    public class SettingsViewModel : ISettingsViewModel
    {
        //properties
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

        private HttpClient _httpClient;

        //methods
        public SettingsViewModel()
        {
        }
        public SettingsViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GetPerfil()
        {
            Usuario usuario = await _httpClient.GetFromJsonAsync<Usuario>("usuario/getperfil");
            CargarObjetoActual(usuario);
        }

        public async Task<HttpResponseMessage> GetLocalizacion()
        {
            HttpResponseMessage _response = await _httpClient.GetAsync("usuario/getlocalizacion");

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                this.Localizacion = await _response.Content.ReadFromJsonAsync<Localizacion>();
            }
            return _response;
        }

        public int GetEstadoTickets()
        {
            return this.EstadoTickets;
        }

        public string GetEstadoMenuTickets()
        {
            return this.EstadoMenuTickets;
        }

        public int GetEstadoCuentaTickets()
        {
            return this.EstadoCuentaTickets;
        }

        private void CargarObjetoActual(SettingsViewModel settingsViewModel)
        {
            this.TemaOscuro = settingsViewModel.TemaOscuro;
            this.Notificaciones = settingsViewModel.Notificaciones;
        }

        public static implicit operator SettingsViewModel(Usuario user)
        {
            return new SettingsViewModel
            {
                Notificaciones = (user.Notificaciones == null || (long)user.Notificaciones == 0) ? false : true,
                TemaOscuro = (user.TemaOscuro == null || (long)user.TemaOscuro == 0) ? false : true
            };
        }
        public static implicit operator Usuario(SettingsViewModel settingsViewModel)
        {
            return new Usuario
            {
                Notificaciones = settingsViewModel.Notificaciones ? 1 : 0,
                TemaOscuro = settingsViewModel.TemaOscuro ? 1 : 0
            };
        }
    }
}
