using System;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class MensajeChatUsuario
	{
		public long MensajeChatId { get; set; }
		[JsonIgnore]
		public MensajeChat MensajeChat { get; set; }

		public Guid UsuarioId { get; set; }
		[JsonIgnore]
		public Usuario Usuario { get; set; }

		public bool Leido { get; set; }
		public DateTime FechaLeido { get; set; }
	}
}
