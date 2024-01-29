using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using Presentacion.Context;
using Presentacion.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly IConfiguration Configuration;
        private readonly AplicationDbContext _context;
        private readonly CryptoService _cryptoService;

        public AuthController(
            AplicationDbContext context,
            IConfiguration configuration,
            CryptoService cryptoService)
        {
            _context = context;
            _cryptoService = cryptoService;
            Configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Usuario usuario)
        {
            try
            {
                var usuarioExiste = await _context.Usuario
                    .Where(u => u.Username == usuario.Username).FirstOrDefaultAsync();

                if (usuarioExiste is null)
                {
                    return Ok(new Respuesta(false, "Usuario no existe"));
                }

                if (!_cryptoService.VerifyPassword(usuario.Password, usuarioExiste.Password))
                {
                    return Ok(new Respuesta(false, "Credenciales incorrectas"));
                }

                return Ok(new Respuesta(true, JsonConvert.SerializeObject(CrearToken(usuarioExiste))));

            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta(false, ex.Message));
            }
        }

        private string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username)
            };
            var appSettingsToken = Configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
     


    } 
}

