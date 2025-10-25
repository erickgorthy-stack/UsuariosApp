using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using UsuariosApp.Domain.Dtos.Requests;

namespace UsuariosApp.Tests
{
    public class UsuariosTest
    {
        //Atributos.
        private readonly HttpClient _client;
        private readonly Faker _Faker;

        //Método construtor.
        public UsuariosTest()
        {
            _client = new WebApplicationFactory<Program>().CreateClient();
            _Faker = new Faker("pt_BR");
        }

        [Fact(
            DisplayName = "Deve criar um novo usuário com sucesso."
            )]
        public void DeveCriarUsuarioComSucesso()
        {
            //Arrange
            //Criando dados da requisição
            var request = new CriarContaRequest(
                Nome: _Faker.Name.FullName(),
                Email: _Faker.Internet.Email(),
                Senha: "@Teste2025"
                );

            //Act
            //Enviar requisição para o endpoint de criação de conta
            var response = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Assert
            //Verificar se a resposta foi bem sucedida e retornou Http 201 Created.
            response.StatusCode.Should().Be(HttpStatusCode.Created);


        }



        [Fact(
           DisplayName = "Não deve permitir criar usuários com o mesmo email.",
           Skip = "Não implementado."
       )]
        public void NaoDevePermitirCriarUsuariosComMesmoEmail()
        {

        }

        [Fact(
           DisplayName = "Deve obrigar o preenchimento de senha forte.",
           Skip = "Não implementado."
       )]
        public void DeveObrigarPreenchimentoDeSenhaForte()
        {

        }

        [Fact(
           DisplayName = "Deve autenticar um usuário com sucesso.",
           Skip = "Não implementado."
       )]
        public void DeveAutenticarUsuarioComSucesso()
        {

        }

        [Fact(
            DisplayName = "Deve retornar acesso negado para usuário inválido.",
            Skip = "Não implementado."
        )]
        public void DeveRetornarAcessoNegadoParaUsuarioInvalido()
        {

        }
    }
}
