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
	public class EmpresaExternaViewModel : IEmpresaExternaViewModel
	{

		public ICollection<EmpresaExterna> EmpresasExternas { get; set; }
		private HttpClient _httpClient;

		public EmpresaExternaViewModel()
		{
		}

		public EmpresaExternaViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
		/// Devuelve una respuesta con el listado de departamentos
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetEmpresasExternas()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("empresaexterna/getempresasexternas");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<List<EmpresaExterna>>());
			}
			return _response;
		}

		private void CargarObjetoActual(EmpresaExternaViewModel empresaExternaViewModel)
		{
			this.EmpresasExternas = empresaExternaViewModel.EmpresasExternas;
		}

		public static implicit operator EmpresaExternaViewModel(List<EmpresaExterna> empresasExternas)
		{
			return new EmpresaExternaViewModel
			{
				EmpresasExternas = empresasExternas
			};
		}

		public static implicit operator List<EmpresaExterna>(EmpresaExternaViewModel empresaExternaViewModel)
		{
			return new List<EmpresaExterna>(empresaExternaViewModel.EmpresasExternas);
		}
	}
}