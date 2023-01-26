using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
    public partial class Usuario
    {
        public Guid UsuarioId { get; set; }
        public string Identificador { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Fuente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string FotoPerfil { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string Extension { get; set; }
        public string FechaNacimiento { get; set; }
        public string SobreMi { get; set; }
        public long? Notificaciones { get; set; }
        public long? TemaOscuro { get; set; }
        public string FechaCreado { get; set; }

        //Chat
        public bool EstaConectado { get; set; }

        [JsonIgnore]
        public virtual Departamento Departamento { get; set; }

        public long DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; }
        public string Rol { get; set; }

        [JsonIgnore]
        public virtual ICollection<DispositivoUsuario> Dispositivos { get; set; }

        [JsonIgnore]
        public virtual ICollection<Jornada> Jornadas { get; set; }

        [JsonIgnore]
        public ICollection<ArchivoUsuario> Archivos { get; set; }

        [JsonIgnore]
        public ICollection<GrupoChatUsuario> GruposChat { get; set; }

        [JsonIgnore]
        public ICollection<MensajeChatUsuario> EstadosMensajes { get; set; }
    }
}
