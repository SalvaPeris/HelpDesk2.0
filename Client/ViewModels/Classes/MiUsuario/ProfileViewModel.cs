using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public class ProfileViewModel : IProfileViewModel
    {
        private HttpClient _httpClient;
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
        public Departamento Departamento { get; set; }
        public long DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; }
        public string FechaCreado { get; set; }
        public string Rol { get; set; }

        public NotificationSeverity NotificacionSeveridad { get; set; }
        public string Mensaje { get; set; }

        public ProfileViewModel()
        {}

        public ProfileViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Anular()
        {
            this.UsuarioId = new Guid();
            this.Identificador = null;
            this.Nombre = null;
            this.Apellidos = null;
            this.Email = null;
            this.SobreMi = null;
            this.FotoPerfil = null;
            this.Telefono = null;
            this.Telefono2 = null;
            this.Extension = null;
            this.FechaNacimiento = null;
            this.Contrasena = null;
            this.DepartamentoId = 0;
            this.DepartamentoNombre = null;
            this.Rol = null;
        }

        public async Task<HttpResponseMessage> ActualizarPerfil()
        {
            return await _httpClient.PutAsJsonAsync("usuario/actualizarperfil", this);
        }

        /// <summary>
        /// Devuelve el perfil actual
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetPerfil()
        {
            HttpResponseMessage _response = await _httpClient.GetAsync("usuario/getperfil");

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Usuario>());
            }
            return _response;
        }

        private void CargarObjetoActual(ProfileViewModel profileViewModel)
        {
            this.UsuarioId = profileViewModel.UsuarioId;
            this.Identificador = profileViewModel.Identificador;
            this.Nombre = profileViewModel.Nombre;
            this.Apellidos = profileViewModel.Apellidos;
            this.Email = profileViewModel.Email;
            this.SobreMi = profileViewModel.SobreMi;
            this.FotoPerfil = profileViewModel.FotoPerfil;
            this.Telefono = profileViewModel.Telefono;
            this.Telefono2 = profileViewModel.Telefono2;
            this.Extension = profileViewModel.Extension;
            this.FechaNacimiento = profileViewModel.FechaNacimiento;
            this.Contrasena = profileViewModel.Contrasena;
            this.DepartamentoId = profileViewModel.DepartamentoId;
            this.DepartamentoNombre = profileViewModel.DepartamentoNombre;
            this.Rol = profileViewModel.Rol;
        }

        public static implicit operator ProfileViewModel(Usuario usuario)
        {
            return new ProfileViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Identificador = usuario.Identificador,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                FechaNacimiento = usuario.FechaNacimiento,
                SobreMi = usuario.SobreMi,
                FotoPerfil = usuario.FotoPerfil,
                Telefono = usuario.Telefono,
                Telefono2 = usuario.Telefono2,
                Extension = usuario.Extension,
                Contrasena = usuario.Contrasena,
                Departamento = usuario.Departamento,
                DepartamentoId = usuario.DepartamentoId,
                DepartamentoNombre = usuario.DepartamentoNombre,
                Rol = usuario.Rol
            };
        }

        public static implicit operator Usuario(ProfileViewModel profileViewModel)
        {
            return new Usuario
            {
                UsuarioId = profileViewModel.UsuarioId,
                Identificador = profileViewModel.Identificador,
                Nombre = profileViewModel.Nombre,
                Apellidos = profileViewModel.Apellidos,
                Email = profileViewModel.Email,
                FechaNacimiento = profileViewModel.FechaNacimiento,
                SobreMi = profileViewModel.SobreMi,
                FotoPerfil = profileViewModel.FotoPerfil,
                Telefono = profileViewModel.Telefono,
                Telefono2 = profileViewModel.Telefono2,
                Extension = profileViewModel.Extension,
                Contrasena = profileViewModel.Contrasena,
                Departamento = profileViewModel.Departamento,
                DepartamentoId = profileViewModel.DepartamentoId,
                DepartamentoNombre = profileViewModel.DepartamentoNombre,
                Rol = profileViewModel.Rol
            };
        }
    }
}