using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        private HttpClient _httpClient;
        public Guid UsuarioId { get; set; }
        public string Identificador { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string Extension { get; set; }
        public string Telefono { get; set; }

        public MainViewModel()
        {
        }

        public MainViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task GetUsuarioActual()
        {
            HttpResponseMessage _response = await _httpClient.GetAsync("usuario/getusuarioactual");

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Usuario>());
            }
        }

        public void CargarObjetoActual(MainViewModel mainViewModel)
        {
            UsuarioId = mainViewModel.UsuarioId;
            this.Identificador = mainViewModel.Identificador;
            this.Email = mainViewModel.Email;
            this.Nombre = mainViewModel.Nombre;
            this.Rol = mainViewModel.Rol;
            this.Apellidos = mainViewModel.Apellidos;
            this.Extension = mainViewModel.Extension;
            this.Telefono = mainViewModel.Telefono;
        }

        public static implicit operator Usuario(MainViewModel mainViewModel)
        {
            return new Usuario
            {
                UsuarioId = mainViewModel.UsuarioId,
                Identificador = mainViewModel.Identificador,
                Email = mainViewModel.Email,
                Nombre = mainViewModel.Nombre,
                Apellidos = mainViewModel.Apellidos,
                Rol = mainViewModel.Rol,
                Extension = mainViewModel.Extension,
                Telefono = mainViewModel.Telefono
            };
        }

        public static implicit operator MainViewModel(Usuario user)
        {
            return new MainViewModel
            {
                UsuarioId = user.UsuarioId,
                Identificador = user.Identificador,
                Email = user.Email,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                Rol = user.Rol,
                Extension = user.Extension,
                Telefono = user.Telefono
            };
        }
    }
}
