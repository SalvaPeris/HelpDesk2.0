using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Situacion
	{
		public long SituacionId { get; set; }
		public string Nombre { get; set; }
		public string Observaciones { get; set; }
		[JsonIgnore]
		public Zona Zona { get; set; }
		public long ZonaId { get; set; }
	}
}