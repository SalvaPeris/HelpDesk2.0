using System;
namespace HelpDesk.Shared.Models
{
	public class Configuracion
	{
		public string NombreServidorSMTP { get; set; }
		public string NombreAMostrarSMTP { get; set; }
		public string NombreUsuarioSMTP { get; set; }
		public string ContrasenaSMTP { get; set; }
		public int PuertoSMTP { get; set; }
	}
}

