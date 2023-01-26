using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public interface IProfileViewModel
    {
        public Guid UsuarioId { get; set; }
        public string Identificador { get; set; }
        [Required(ErrorMessage = "El correo electrónico es necesario")]
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Fuente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string FotoPerfil { get; set; }
        [Required(ErrorMessage = "El teléfono es necesario")]
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string Extension { get; set; }
        public string FechaNacimiento { get; set; }
        public string SobreMi { get; set; }
        public long? Notificaciones { get; set; }
        public long? TemaOscuro { get; set; }
        public string FechaCreado { get; set; }
        public Departamento Departamento { get; set; }
        public long DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; }
        public string Rol { get; set; }

        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }

        public Task<HttpResponseMessage> ActualizarPerfil();
        public Task<HttpResponseMessage> GetPerfil();
        public void Anular();
    }
}