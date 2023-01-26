using System;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class DispositivoUsuario
	{
		public Guid DispositivoId { get; set; }
		[JsonIgnore]
		public Dispositivo Dispositivo { get; set; }

		public Guid UsuarioId { get; set; }
		[JsonIgnore]
		public Usuario Usuario { get; set; }
	}
}

