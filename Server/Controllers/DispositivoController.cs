using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Server.Repository;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DispositivoController : Controller
    {
        private readonly IDispositivoRepository _dispositivoRepository;

        public DispositivoController(HelpDeskContext context)
        {
            this._dispositivoRepository = new DispositivoRepository(context);
        }

        [HttpGet("GetDispositivosPorUsuario")]
        public async Task<ActionResult<ICollection<Dispositivo>>> GetDispositivosPorIdUsuario(Guid usuarioId)
        {
            ICollection<Dispositivo> dispositivos = await _dispositivoRepository.GetDispositivosPorIdUsuario(usuarioId);

            if (dispositivos.Count >= 0)
            {
                return Ok(dispositivos);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Trae los dispositivos del usuario actual
        /// </summary>
        /// <returns></returns>
        [HttpGet("MisDispositivos")]
        public async Task<ActionResult<ICollection<Dispositivo>>> GetDispositivos()
        {
            ICollection<Dispositivo> dispositivos = await _dispositivoRepository.GetDispositivosPorIdUsuario(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (dispositivos.Count >= 0)
            {
                return Ok(dispositivos);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Trae dispositivo según Identificador
        /// </summary>
        /// <param name="dispositivoId"></param>
        /// <returns></returns>
        [HttpGet("GetDispositivoPorId")]
        public async Task<ActionResult<Dispositivo>> GetDispositivoPorId(Guid dispositivoId)
        {
            Dispositivo dispositivo = null;

            if (User.IsInRole("SuperAdministrador") || User.IsInRole("Administrador"))
            {
                dispositivo = await _dispositivoRepository.GetDispositivoPorId(dispositivoId);
            }

            if (dispositivo != null)
            {
                return Ok(dispositivo);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Trae los dispositivos por departamento
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDispositivos")]
        public async Task<ActionResult<ICollection<Dispositivo>>> GetDispositivosPorDepartamento()
        {
            ICollection<Dispositivo> dispositivos = null;

            if (User.IsInRole("SuperAdministrador") || User.IsInRole("Administrador"))
            {
                dispositivos = await _dispositivoRepository.GetDispositivos();
            }

            if (dispositivos.Count >= 0)
            {
                return Ok(dispositivos);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Actualiza el dispositivo
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Dispositivo dispositivo)
        {
            Dispositivo _dispositivoActualizar = await _dispositivoRepository.GetDispositivoPorId(dispositivo.DispositivoId);

            if (_dispositivoActualizar == null)
            {
                return null;
            }
            else
            {
                _dispositivoActualizar.NumeroSerie = dispositivo.NumeroSerie;
                _dispositivoActualizar.Marca = dispositivo.Marca;
                _dispositivoActualizar.Modelo = dispositivo.Modelo;
                _dispositivoActualizar.Licencia = dispositivo.Licencia;
                _dispositivoActualizar.Situacion = dispositivo.Situacion;
                _dispositivoActualizar.FechaInstalado = dispositivo.FechaInstalado;
                _dispositivoActualizar.CompradoA = dispositivo.CompradoA;
                _dispositivoActualizar.Observaciones = dispositivo.Observaciones;
                _dispositivoActualizar.FuncionaBien = dispositivo.FuncionaBien;
                _dispositivoActualizar.Utilizado = dispositivo.Utilizado;
                _dispositivoActualizar.LlevaMantenimiento = dispositivo.LlevaMantenimiento;
                _dispositivoActualizar.FechaCreado = dispositivo.FechaCreado;
                _dispositivoActualizar.Tipo = dispositivo.Tipo;
                _dispositivoActualizar.Usuarios = dispositivo.Usuarios;
                _dispositivoActualizar.Estados = dispositivo.Estados;
                _dispositivoActualizar.DepartamentoId = dispositivo.DepartamentoId;
                _dispositivoActualizar.EmpresaExternaId = dispositivo.EmpresaExternaId;
            }

            var result = await _dispositivoRepository.Guardar();

            if (result >= 0)
            {
                return Ok(_dispositivoActualizar);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Crea un nuevo dispositivo
        /// </summary>
        /// <param name="dispositivo"></param>
        /// <returns></returns>
        [HttpPut("Nuevo")]
        public async Task<ActionResult<Dispositivo>> NuevoDispositivo([FromBody] Dispositivo dispositivo)
        {
            var result = await _dispositivoRepository.Nuevo(dispositivo);
            if (result > 0)
            {
                return Ok(dispositivo);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza el perfil del dispositivo actual
        /// </summary>
        /// <param name="dispositivo"></param>
        /// <returns></returns>
        [HttpPut("Eliminar")]
        public async Task<IActionResult> EliminarDispositivo([FromBody] Dispositivo dispositivo)
        {
            var result = 0;

            if (User.IsInRole("SuperAdministrador") || (User.IsInRole("Administrador")))
            {
                result = await _dispositivoRepository.Eliminar(dispositivo);
            }

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
