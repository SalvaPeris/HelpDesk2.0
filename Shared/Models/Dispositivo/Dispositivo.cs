using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
    public partial class Dispositivo
    {
        public Guid DispositivoId { get; set; }
        public string NumeroSerie { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Licencia { get; set; }
        public string Situacion { get; set; }
        public DateTime FechaInstalado { get; set; }
        public string CompradoA { get; set; }
        public string Observaciones { get; set; }
        public bool FuncionaBien { get; set; }
        public bool Utilizado { get; set; }
        public bool LlevaMantenimiento { get; set; }
        public DateTime FechaCreado { get; set; }
        public long TipoDispositivoId { get; set; }
        public string TipoNombre { get; set; }
        public TipoDispositivo Tipo { get; set; }

        public int UsuariosCuenta { get; set; }
        public ICollection<DispositivoUsuario> Usuarios { get; set; }

        public ICollection<EstadoDispositivo> Estados { get; set; }

        public string DepartamentoNombre { get; set; }
        public Departamento Departamento { get; set; }
        public long DepartamentoId { get; set; }

        public string EmpresaExternaNombre { get; set; }
        public EmpresaExterna EmpresaExterna { get; set; }
        public long EmpresaExternaId { get; set; }
    }
}
