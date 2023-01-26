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
    public class RecuperarContrasenaViewModel : IRecuperarContrasenaViewModel
    {
        [Required(ErrorMessage = "Identificador necesario")]
        public string Identificador { get; set; }

        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }
        private HttpClient _httpClient;

        public RecuperarContrasenaViewModel()
        {}

        public RecuperarContrasenaViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ResetContrasena()
        {
            Usuario usuario = await _httpClient.GetFromJsonAsync<Usuario>("usuario/recuperarcontrasena?identificador=" + Identificador);

            Console.WriteLine("Recibido: " + usuario.Email);

            if (usuario == null)
            {
                this.Mensaje = "Ha ocurrido un error. Inténtalo más tarde.";
                this.NotificacionSeveridad = NotificationSeverity.Error;
            }
            else
            {
                this.Mensaje = "Se ha enviado la contraseña al correo electrónico " + usuario.Email + ".";
                this.NotificacionSeveridad = NotificationSeverity.Success;
            }
        }

        public static implicit operator RecuperarContrasenaViewModel(Usuario usuario)
        {
            return new RecuperarContrasenaViewModel
            {
                Identificador = usuario.Identificador
             };
        }

        public static implicit operator Usuario(RecuperarContrasenaViewModel loginViewModel)
        {
            return new Usuario
            {
                Identificador = loginViewModel.Identificador
            };
        }
    }
}