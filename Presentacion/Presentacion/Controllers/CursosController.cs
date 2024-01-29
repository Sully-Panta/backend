using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Presentacion.Context;
using Presentacion.Shared;
using System.Transactions;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : Controller
    {

        private readonly AplicationDbContext _context;

        public CursosController
            (AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Curso>> Get()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    List<Curso> cursos = await _context.Curso.ToListAsync();
                    return Ok(new Respuesta(true, cursos));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta(false, ex.Message));
                }
            }
        }

    }
}
