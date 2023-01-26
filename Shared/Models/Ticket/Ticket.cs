using System;
using System.Collections.Generic;

namespace HelpDesk.Shared.Models
{
    public partial class Ticket
    {
        public long TicketId { get; set; }
        public string Titulo { get; set; }
        public string Area { get; set; }
        public string TipoTicket { get; set; }
        public bool Publico { get; set; }
        public string Observaciones { get; set; }
        public string Imagen { get; set; }
        public string Gravedad { get; set; }
        public string Estado { get; set; }
        public string RemotoApp { get; set; }
        public string RemotoID { get; set; }
        public string RemotoContrasena { get; set; }
        public string CreadoPor { get; set; }
        public string CreadoPorNombreCompleto { get; set; }
        public string AsignadoA { get; set; }
        public string AsignadoANombreCompleto { get; set; }
        public string TelefonoContacto { get; set; }
        public string EmailContacto { get; set; }
        public string Departamento { get; set; }
        public string Dispositivo { get; set; }
        public DateTime FechaCreado { get; set; }
        public string FechaSolucionado { get; set; }
        public string SolucionadoPor { get; set; }
        public string SolucionadoPorNombreCompleto { get; set; }

        public int SuplementosCuenta { get; set; }
        public ICollection<SuplementoTicket> Suplementos { get; set; }

        public int EstadosCuenta { get; set; }
        public ICollection<EstadoTicket> Estados { get; set; }

        public int ZonasCuenta { get; set; }
        public ICollection<ZonaTicket> Zonas { get; set; }
    }
}

