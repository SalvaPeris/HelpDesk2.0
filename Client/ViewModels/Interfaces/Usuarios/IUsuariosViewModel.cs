using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.ViewModels
{
	public interface IUsuariosViewModel
	{
		public Usuario[] Usuarios { get; set; }

		public Task<HttpResponseMessage> GetUsuarios();
	}
}