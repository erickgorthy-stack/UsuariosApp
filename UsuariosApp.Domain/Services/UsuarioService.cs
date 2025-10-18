using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Dtos.Requests;
using UsuariosApp.Domain.Dtos.Response;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Validators;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        /// <summary>
        /// Classe de serviço para operações relacionadas a Usuário.
        /// </summary>

        // Atributos
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        public CriarContaResponse CriarConta(CriarContaRequest request)
        {
            // Capturar os dados do usuário.
            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha,
               

            };

            // Validar dados do usuário.
            var Validator = new UsuarioValidator(_usuarioRepository);
            var result = Validator.Validate(usuario);

            // Verificar se a validação falhou.
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors); // Menssagens de erro
            }
            
            //criptografar a senha do usuário
            usuario.Senha = CryptoHelper.GetSHA256(usuario.Senha);

            //Buscando o perfil com o nome 'USUARIO'
            var perfil = _perfilRepository.Get("USUARIO");

            //Associar o usuário a um perfil padrão (Usuario)
            if (perfil != null)
                usuario.PerfilId = perfil.Id;

            //Salvar o usuário no banco de dados
            _usuarioRepository.Add(usuario);

            //Retornar os dados do usuário criado
            return new CriarContaResponse(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                perfil != null ? perfil.Nome : string.Empty,
                DateTime.Now
                );
        }

        AutenticarUsuarioResponse IUsuarioService.AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            //Buscar o usuário no banco de dados através do email e da senha
            var usuario = _usuarioRepository.Get(request.Email, CryptoHelper.GetSHA256(request.Senha));

            //Verificando se o usuário não foi encontrado
            if (usuario == null)
            {
                throw new ApplicationException("Usuário ou senha inválidos.");
            }

            //Retornar os dados do usuário autenticado
            return new AutenticarUsuarioResponse(
                    usuario.Id,     //Id do usuário
                    usuario.Nome,   //Nome do usuário
                    usuario.Email,  //Email do usuário
                    usuario.Perfil.Nome,    //Nome do perfil
                    DateTime.Now,    //Data e hora de acesso
                    JwtTokenHelper.GenerateToken(usuario.Email, usuario.Perfil.Nome)
                );
        }
    }
}
