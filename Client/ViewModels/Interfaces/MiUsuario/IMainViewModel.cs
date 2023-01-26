using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelpDesk.ViewModels
{
    public interface IMainViewModel
    {
        public Guid UsuarioId { get; set; }
        public string Identificador { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string Extension { get; set; }
        public string Telefono { get; set; }

        public Task GetUsuarioActual();
    }
}
