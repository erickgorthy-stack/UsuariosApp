using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Requests
{
   

        /// <summary>
        /// DTO para a entrada de dados do serviço 
        /// de criação de conta de usuário
        /// </summary>
        public record CriarContaRequest(
            string Nome,    //Nome do usuário
            string Email,   //Email do usuário
            string Senha    //Senha do usuário
            )
        {

        }

    
}
