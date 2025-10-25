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

        //M�todo construtor.
        public UsuariosTest()
        {
            _client = new WebApplicationFactory<Program>().CreateClient();
            _Faker = new Faker("pt_BR");
        }

        [Fact(
            DisplayName = "Deve criar um novo usu�rio com sucesso."
            )]
        public void DeveCriarUsuarioComSucesso()
        {
            //Arrange
            //Criando dados da requisi��o
            var request = new CriarContaRequest(
                Nome: _Faker.Name.FullName(),
                Email: _Faker.Internet.Email(),
                Senha: "@Teste2025"
                );

            //Act
            //Enviar requisi��o para o endpoint de cria��o de conta
            var response = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Assert
            //Verificar se a resposta foi bem sucedida e retornou Http 201 Created.
            response.StatusCode.Should().Be(HttpStatusCode.Created);


        }



        [Fact(
           DisplayName = "N�o deve permitir criar usu�rios com o mesmo email.",
           Skip = "N�o implementado."
       )]
        public void NaoDevePermitirCriarUsuariosComMesmoEmail()
        {

        }

        [Fact(
           DisplayName = "Deve obrigar o preenchimento de senha forte.",
           Skip = "N�o implementado."
       )]
        public void DeveObrigarPreenchimentoDeSenhaForte()
        {

        }

        [Fact(
           DisplayName = "Deve autenticar um usu�rio com sucesso.",
           Skip = "N�o implementado."
       )]
        public void DeveAutenticarUsuarioComSucesso()
        {

        }

        [Fact(
            DisplayName = "Deve retornar acesso negado para usu�rio inv�lido.",
            Skip = "N�o implementado."
        )]
        public void DeveRetornarAcessoNegadoParaUsuarioInvalido()
        {

        }
    }
}
