using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Dtos.Requests;
using UsuariosApp.Domain.Dtos.Response;

namespace UsuariosApp.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface para abstração de métodos que serão
    /// implementados no UsuarioService.
    /// </summary>

    public interface IUsuarioService
    {
        CriarContaResponse CriarConta(CriarContaRequest request);
    }
}
