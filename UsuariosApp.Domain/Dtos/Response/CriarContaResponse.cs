using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Response
{
   
        /// <summary>
        /// DTO para a saída de dados do serviço 
        /// de criação de conta de usuário
        /// </summary>
        public record CriarContaResponse(
            Guid id,             //Identificador único do usuário
            string Nome,         //Nome do usuário
            string Email,        //Email do usuário
            string Perfil,       //Perfil do usuário
            DateTime DataCriacao //Data de criação da conta
            )
        {
        }
    
}
