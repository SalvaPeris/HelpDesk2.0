using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen.Blazor;

namespace HelpDesk.ViewModels
{
    public class GrupoChatViewModel : IGrupoChatViewModel
    {
        public GrupoChat GrupoChat { get; set; }
        public EstadoChat EstadoPantallaChat { get; set; }

        private HttpClient _httpClient;

        public GrupoChatViewModel()
        {
        }

        public GrupoChatViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            GrupoChat = new GrupoChat()
            {
                Mensajes = new List<MensajeChat>() { new MensajeChat() { Mensaje = "Bienvenido al chat", CreadoPor = new Usuario () { Nombre = "Sistema" } } }
            };
        }


        public async Task<HttpResponseMessage> GetHistorialChat(Guid usuarioId)
        {
            HttpResponseMessage _response = await _httpClient.GetAsync("chat/usuario/" + usuarioId);

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                CargarObjetoActual(await _response.Content.ReadFromJsonAsync<GrupoChat>());
            }

            return _response;
        }

        public async Task<HttpResponseMessage> GetHistorialChatGrupal(Guid grupoId)
        {
            HttpResponseMessage _response = await _httpClient.GetAsync("chat/grupo/" + grupoId);

            if (_response.StatusCode == HttpStatusCode.OK)
            {
                CargarObjetoActual(await _response.Content.ReadFromJsonAsync<GrupoChat>());
            }

            return _response;
        }


        public async Task<HttpResponseMessage> NuevoMensaje(MensajeChat mensajeChat)
        {
            mensajeChat.GrupoChatId = this.GrupoChat.GrupoChatId;

            HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("chat/nuevo", mensajeChat);

            return _response;
        }


        private void CargarObjetoActual(GrupoChatViewModel grupoChatViewModel)
        {
            this.GrupoChat = grupoChatViewModel.GrupoChat;
        }

        public static implicit operator GrupoChatViewModel(GrupoChat grupoChat)
        {
            return new GrupoChatViewModel
            {
                GrupoChat = grupoChat
            };
        }

        public static implicit operator GrupoChat(GrupoChatViewModel mensajesChatViewModel)
        {
            return mensajesChatViewModel.GrupoChat;
        }
    }

    public enum EstadoChat
    {
        DentroChat,
        FueraChat
    }
}

