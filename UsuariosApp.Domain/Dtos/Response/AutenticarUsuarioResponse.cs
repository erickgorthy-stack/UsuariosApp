using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Response
{
    /// <summary>
    /// DTO para a saída de dados do serviço de autenticação de usuário
    /// </summary>
    public record AutenticarUsuarioResponse
    (
        Guid Id,           //Identificador único do usuário
        string Nome,       //Nome do usuário
        string Email,      //Email do usuário
        string Perfil,     //Perfil de acesso do usuário
        DateTime DataHoraAcesso, //Data e hora do acesso
        string AcessToken //Token JWT de acesso
    );
    
}
