using System;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class ArchivoUsuario
	{
		public long ArchivoId { get; set; }
		[JsonIgnore]
		public Archivo Archivo { get; set; }

		public Guid UsuarioId { get; set; }
		[JsonIgnore]
		public Usuario Usuario { get; set; }
	}
}

