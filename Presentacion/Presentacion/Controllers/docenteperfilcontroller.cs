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
    public class docenteperfilcontroller : Controller
    {
        private readonly AplicationDbContext _context;

        public docenteperfilcontroller
            (AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Docenteperfil>> Get()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    List<Docenteperfil> Docnetep = await _context.Docenteperfil.ToListAsync();
                    return Ok(new Respuesta(true, Docnetep));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta(false, ex.Message));
                }
            }
        }
    }
}
