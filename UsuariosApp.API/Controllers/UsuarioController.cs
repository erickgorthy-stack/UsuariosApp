using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Domain.Dtos.Requests;
using UsuariosApp.Domain.Dtos.Response;
using UsuariosApp.Domain.Interfaces.Services;
using FluentValidation;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // Atributo
        private readonly IUsuarioService _usuarioService;

        // Método Construtor para injeção de dependência
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("criar")]
        [ProducesResponseType(typeof(CriarContaResponse), 200)]
        public IActionResult Criar([FromBody] CriarContaRequest request)
        {
            try
            {
                //Executando o serviço de criação de conta do usuário
                var response = _usuarioService.CriarConta(request);

                //HTTP 201 - CREATED
                return CreatedAtAction(nameof(Criar), response);
            }
            catch (ValidationException e)
            {
                //HTTP 400 - Bad Request
                return BadRequest(e.Errors.Select(e => new {e.PropertyName, e.ErrorMessage }));
            }
            catch (Exception e)
            {
                //HTTP 500 - Internal Server Error
                return StatusCode(500, e.Message);
            }
        }
    }
}
