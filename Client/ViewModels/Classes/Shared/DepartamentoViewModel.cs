using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public class DepartamentoViewModel : IDepartamentoViewModel
	{

		public List<Departamento> Departamentos { get; set; }
		private HttpClient _httpClient;

		public DepartamentoViewModel()
		{
		}

		public DepartamentoViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
        /// Devuelve una respuesta con el listado de departamentos
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> GetDepartamentos()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("departamento/getdepartamentos");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<Departamento>>());
			}
			return _response;
		}

		private void CargarObjetoActual(DepartamentoViewModel departamentoViewModel)
		{
			this.Departamentos = departamentoViewModel.Departamentos;
		}

		public static implicit operator DepartamentoViewModel(List<Departamento> departamentos)
		{
			return new DepartamentoViewModel
			{
				Departamentos = departamentos
			};
		}

		public static implicit operator List<Departamento>(DepartamentoViewModel departamentoViewModel)
		{
			return new List<Departamento>(departamentoViewModel.Departamentos);
		}
	}
}

