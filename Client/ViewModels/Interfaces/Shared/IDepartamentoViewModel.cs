using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface IDepartamentoViewModel
	{
		public List<Departamento> Departamentos { get; set; }

		public Task<HttpResponseMessage> GetDepartamentos();
	}
}
