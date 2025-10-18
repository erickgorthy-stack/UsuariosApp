using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Requests
{
    /// <summary>
    /// DTO para entrada de dados do serviço de autenticação de usuário
    /// </summary>
    public record AutenticarUsuarioRequest
    (
        string Email,   //Email do usuário
        string Senha    //Senha do usuário
    );
}
