using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface IZonaViewModel
	{
		public List<Zona> Zonas { get; set; }

		public Task<HttpResponseMessage> GetZonas();
	}
}