using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class TipoDispositivo
	{
		public TipoDispositivo()
		{
		}

		public long TipoDispositivoId { get; set; }
		public string Nombre { get; set; }
		public string Imagen { get; set; }
		public string Observaciones { get; set; }

		[JsonIgnore]
		public ICollection<Dispositivo> Dispositivos { get; set; }
	}
}
