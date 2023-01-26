using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Shared;
using HelpDesk.Shared.Models;

namespace HelpDesk.ViewModels
{
    public class AgendaViewModel : IAgendaViewModel
    {

        public Usuario[] Usuarios { get; set; }
        private HttpClient _httpClient;

        public AgendaViewModel()
        {

        }

        public AgendaViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GetUsuarios()
        {
            Usuario[] _usuarios = await _httpClient.GetFromJsonAsync<Usuario[]>("usuario/getusuarios");
            CargarObjetoActual(_usuarios);

            /*if (_usuarios.Length > 0)
            {
                this.Mensaje = "Tickets cargados correctamente";
                this.NotificacionSeveridad = NotificationSeverity.Success;
            }
            else
            {
                this.Mensaje = "Error al cargar los tickets.";
                this.NotificacionSeveridad = NotificationSeverity.Error;
            }*/
        }

        public async Task BuscarUsuario(string busqueda)
        {
            Usuario[] _usuarios = await _httpClient.GetFromJsonAsync<Usuario[]>("usuario/buscar?busqueda=" + busqueda);
            CargarObjetoActual(_usuarios);
        }

        public async Task BuscarUsuarioPorLetra(string busqueda)
        {
            Usuario[] _usuarios = await _httpClient.GetFromJsonAsync<Usuario[]>("usuario/buscarporletra?busqueda=" + busqueda);
            CargarObjetoActual(_usuarios);
        }

        private void CargarObjetoActual(AgendaViewModel agendaViewModel)
        {
            this.Usuarios = agendaViewModel.Usuarios;
        }

        public static implicit operator AgendaViewModel(Usuario[] usuarios)
        {
            return new AgendaViewModel
            {
                Usuarios = usuarios
            };
        }

        public static implicit operator Usuario[](AgendaViewModel agendaViewModel)
        {
            return agendaViewModel.Usuarios;
        }
    }
}