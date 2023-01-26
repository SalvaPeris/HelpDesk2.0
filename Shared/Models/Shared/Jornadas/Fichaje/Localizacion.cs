using System;
namespace HelpDesk.Shared.Models
{
	public class Localizacion
	{
		public Localizacion()
		{
		}

		public long LocalizacionId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Town { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}
}