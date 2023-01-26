using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Shared;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        [Required(ErrorMessage = "Identificador necesario")]
        public string Identificador { get; set; }
        [Required(ErrorMessage = "Contraseña necesaria")]
        public string Contrasena { get; set; }

        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }
        private HttpClient _httpClient;

        public LoginViewModel()
        {

        }
        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginUsuario()
        {
            return await _httpClient.PostAsJsonAsync<Usuario>("usuario/login", this);
        }

        public static implicit operator LoginViewModel(Usuario usuario)
        {
            return new LoginViewModel
            {
                Identificador = usuario.Identificador,
                Contrasena = usuario.Contrasena
             };
        }

        public static implicit operator Usuario(LoginViewModel loginViewModel)
        {
            return new Usuario
            {
                Identificador = loginViewModel.Identificador,
                Contrasena = loginViewModel.Contrasena
            };
        }
    }
}