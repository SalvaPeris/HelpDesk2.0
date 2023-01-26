using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Radzen;
using System.Net.Http;

namespace HelpDesk.ViewModels
{
    public interface ILoginViewModel
    {
        [Required(ErrorMessage = "Identificador necesario")]
        public string Identificador { get; set; }
        [Required(ErrorMessage = "Contraseña necesaria")]
        public string Contrasena { get; set; }
        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }

        public Task<HttpResponseMessage> LoginUsuario();
    }
}