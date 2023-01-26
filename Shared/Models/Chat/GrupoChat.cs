using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class GrupoChat
	{
		public Guid GrupoChatId { get; set; }
		public string Titulo { get; set; }
		public DateTime FechaCreado { get; set; }
		public string Observaciones { get; set; }

		[JsonIgnore]
		public Usuario CreadoPor { get; set; }

		[JsonIgnore]
		public ICollection<GrupoChatUsuario> Usuarios { get; set; }
		public int CuentaUsuarios { get; set; }

		public ICollection<MensajeChat> Mensajes { get; set; }
		public int MensajesSinLeer { get; set; }
	}
}

