using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace HelpDesk.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            HttpResponseMessage _response = await _httpClient.GetAsync("usuario/getusuarioactual");

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                Usuario usuarioActual = await _response.Content.ReadFromJsonAsync<Usuario>();

                var claimId = new Claim(ClaimTypes.NameIdentifier, usuarioActual.UsuarioId.ToString());
                var claimName = new Claim(ClaimTypes.Name, usuarioActual.Nombre);
                var claimSurname = new Claim(ClaimTypes.Surname, usuarioActual.Apellidos);
                var claimRole = new Claim("Role", "Usuario");
                var claimDept = new Claim("GroupSid", usuarioActual.DepartamentoNombre);

                if (usuarioActual.Rol == "SuperAdministrador")
                {
                    claimRole = new Claim("Role", "SuperAdministrador");
                }
                else if (usuarioActual.Rol == "Administrador")
                {
                    claimRole = new Claim("Role", "Administrador");
                }

                var claimsIdentity = new ClaimsIdentity(new[] { claimId, claimName, claimSurname, claimRole, claimDept }, "serverAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            else
            {
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
        }         
    }
} 