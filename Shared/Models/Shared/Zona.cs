using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Zona
	{
		public Zona()
		{
		}

		public long ZonaId { get; set; }
		public string Nombre { get; set; }
		public string Observaciones { get; set; }
		public ICollection<Situacion> Situaciones { get; set; }
	}
}