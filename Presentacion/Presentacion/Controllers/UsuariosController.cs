using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Presentacion.Context;
using Presentacion.Shared;
using System.Transactions;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class UsuariosController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly CryptoService _cryptoService;

        public UsuariosController
            (AplicationDbContext context, 
            CryptoService cryptoService)
        {
            _context = context;
            _cryptoService = cryptoService;
        }
       
        [HttpPost]
        public async Task<ActionResult<Respuesta>> Post([FromBody] Usuario nuevo)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    bool usernameExiste = await _context.Usuario.AnyAsync(x => x.Username.ToUpper() == nuevo.Username.ToUpper());
                    if (usernameExiste)
                    {
                        return Ok(new Respuesta(false, "Nombre de usuario ya registrado"));
                    }

                    nuevo.Password = _cryptoService.HashPassword(nuevo.Password);
                    
                    _context.Usuario.Add(nuevo);
                    await _context.SaveChangesAsync();

                    scope.Complete();

                    return Ok(new Respuesta(true, "Usuario ingresado con éxito."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta(false, ex.Message));
                }
            }
        }
    }
}
