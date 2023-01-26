using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public partial class EmpresaExterna
	{
		public long EmpresaExternaId { get; set; }
		public string Nombre { get; set; }
		public string TelefonoContacto { get; set; }
		public string Direccion { get; set; }
		public string Observaciones { get; set; }
		[JsonIgnore]
		public ICollection<Dispositivo> Dispositivos { get; set; }
	}
}
