using System;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class GrupoChatUsuario
	{
		public Guid GrupoChatId { get; set; }
		[JsonIgnore]
		public GrupoChat GrupoChat { get; set; }

		public Guid UsuarioId { get; set; }
		[JsonIgnore]
		public Usuario Usuario { get; set; }
	}
}

