using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Helpers
{
    /// <summary>
    /// Métodos auxiliares para geração de tokens JWT.
    /// </summary>
    public class JwtTokenHelper
    {
        public static string GenerateToken(string email, string perfil)
        {
            // Chave secreta usada para assinar o token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cotiinformatica-usuariosApp-123456789@2025"));
            
            // Criptografar a assinatura do token
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Informação do usuário do Token
            var claims = new[]
            {
               new Claim(ClaimTypes.Name, email), //Nome do usuário autenticado
               new Claim(ClaimTypes.Role, perfil) //Perfil do usuário autenticado
            };

            //Criando Token JWT
            var token = new JwtSecurityToken(
                claims: claims, //informações do usuário do token
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                );

            //retornando o TOKEN
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
