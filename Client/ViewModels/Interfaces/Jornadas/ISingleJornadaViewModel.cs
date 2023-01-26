using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public interface ISingleJornadaViewModel
    {
        public long JornadaId { get; set; }
        public DateTime FechaJornada { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public string UsuarioNombreCompleto { get; set; }
        public string ImagenUsuario { get; set; }

        public ICollection<Fichaje> Fichajes { get; set; }

        public int AusenciaCuenta { get; set; }
        public ICollection<JornadaAusencia> Ausencias { get; set; }

        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }

        public Jornada GetSingleJornada();
        public void SetSingleJornada(Jornada jornada);

        public Task<HttpResponseMessage> GetJornadaPorId(string identificador);

        public Task<HttpResponseMessage> NuevaJornada();
        public Task<HttpResponseMessage> EliminarJornada();
        public Task<HttpResponseMessage> ActualizaJornada();
        public void Anular();
    }
}