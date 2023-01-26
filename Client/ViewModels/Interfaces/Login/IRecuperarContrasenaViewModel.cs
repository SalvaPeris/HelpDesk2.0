using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
    public interface IRecuperarContrasenaViewModel
    {
        [Required(ErrorMessage = "Identificador necesario")]
        public string Identificador { get; set; }
        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }

        public Task ResetContrasena();
    }
}
