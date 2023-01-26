using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Server.Repository;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JornadaController : Controller
    {
        private readonly IJornadaRepository _jornadaRepository;

        public JornadaController(HelpDeskContext context)
        {
            this._jornadaRepository = new JornadaRepository(context);
        }

#region "USUARIO ACTUAL"
        /// <summary>
        /// Abre fichaje para Hoy para el usuario actual
        /// </summary>
        /// <param name="fichaje"></param>
        /// <returns></returns>
        [HttpPut("AbrirFichaje")]
        public async Task<ActionResult> AbrirFichaje([FromBody] Jornada jornada)
        {
            var result = await _jornadaRepository.NuevoFichaje(jornada, new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (result >= 0)
            {
                return Ok(jornada);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Finaliza fichaje para Hoy para el usuario actual
        /// </summary>
        /// <param name="fichaje"></param>
        /// <returns></returns>
        [HttpPut("FinalizaFichaje")]
        public async Task<ActionResult> FinalizaFichaje([FromBody] Jornada jornada)
        {
            var result = await _jornadaRepository.FinalizaFichaje(jornada);
            if (result >= 0)
            {
                return Ok(jornada);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Trae los fichajes del usuario actual
        /// </summary>
        /// <returns></returns>
        [HttpGet("MisFichajes")]
        public async Task<ActionResult<ICollection<Jornada>>> GetMisFichajes(DateTime fechaComienzo, DateTime fechaFin)
        {
            ICollection<Jornada> jornadas = await _jornadaRepository.GetJornadasPorIdUsuario(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)), fechaComienzo, fechaFin);

            if (jornadas.Count >= 0)
            {
                return Ok(jornadas);
            }
            else
            {
                return NoContent();
            }
        }

        #endregion "USUARIO ACTUAL"

        /// <summary>
        /// Trae los fichajes de los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFichajesPorDepartamento")]
        public async Task<ActionResult<ICollection<Jornada>>> GetFichajesPorDepartamento()
        {
            ICollection<Jornada> jornadas = await _jornadaRepository.GetJornadasPorDepartamento(1);

            if (jornadas.Count >= 0)
            {
                return Ok(jornadas);
            }
            else
            {
                return NoContent();
            }
        }
    }
}

