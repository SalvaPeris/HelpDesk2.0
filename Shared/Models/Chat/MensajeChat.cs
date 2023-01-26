using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class MensajeChat
	{
		public long MensajeChatId { get; set; }
		public string Mensaje { get; set; }
		public DateTime FechaCreado { get; set; }

		public Usuario CreadoPor { get; set; }

		public Guid GrupoChatId { get; set; }

		[JsonIgnore]
		public GrupoChat GrupoChat { get; set; }

		[JsonIgnore]
		public ICollection<MensajeChatUsuario> Estados { get; set; }
	}
}

