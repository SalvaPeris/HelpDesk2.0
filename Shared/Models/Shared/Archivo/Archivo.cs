using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Archivo
	{
		public long ArchivoId { get; set; }
		public string Nombre { get; set; }
		public DateTime FechaSubido { get; set; }
		public byte[] Adjunto { get; set; }

		public ICollection<ArchivoUsuario> Usuarios { get; set; }
	}
}

