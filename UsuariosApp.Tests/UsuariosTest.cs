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
           DisplayName = "N�o deve permitir criar usu�rios com o mesmo email."
       )]
        public void NaoDevePermitirCriarUsuariosComMesmoEmail()
        {
            //Arrange
            //Criando dados da requisi��o
            var request = new CriarContaRequest(
                Nome: _Faker.Name.FullName(),
                Email: _Faker.Internet.Email(),
                Senha: "@Teste2025"
                );

            //Act
            //Enviar requisi��o para o endpoint de cria��o de cadastro de usuario da API
            var response1 = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;
            response1.StatusCode.Should().Be(HttpStatusCode.Created);

            //Act
            //Tentar criar o mesmo usu�rio novamente
            var response2 = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Assert
            //Verificar se a resposta retornou Http 400 Bad Request
            response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //Assert
            //Verificar se a mensagem de erro est� correta
            var content = response2.Content.ReadAsStringAsync().Result;
            content.Should().Contain("O email informado j� est� em uso.");


        }

        [Fact(
           DisplayName = "Deve obrigar o preenchimento de senha forte."
       )]
        public void DeveObrigarPreenchimentoDeSenhaForte()
        {
            //Arrange
            //Criando dados da requisi��o com senha fraca
            var request = new CriarContaRequest(
                Nome: _Faker.Person.FullName,
                Email: _Faker.Internet.Email(),
                Senha: "123"
                );

            //Act
            //Enviar requisi��o para o endpoint de cria��o de cadastro de usuario da API
            var response = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Verificar se a mensagem de erro est� correta
            var content = response.Content.ReadAsStringAsync().Result;
            content.Should().Contain("A senha deve ter pelo menos 1 letra min�scula," +
                " 1 letra mai�scula, 1 n�mero, 1 s�mbolo e no m�nimo 8 caracteres.");
        }

        [Fact(
           DisplayName = "Deve autenticar um usu�rio com sucesso."
       )]
        public void DeveAutenticarUsuarioComSucesso()
        {
            //Arrange
            //Criando dados da requisi��o para criar o usu�rio
                var criarRequest = new CriarContaRequest(
                Nome: _Faker.Person.FullName,
                Email: _Faker.Internet.Email(),
                Senha: "@Teste2025"
                );
            //Act
            //Enviar requisi��o para o endpoint de cria��o de cadastro de usuario da API
            var responseCriar = _client.PostAsJsonAsync("/api/usuario/criar", criarRequest).Result;

            //Assert
            //Verificar se a resposta foi bem sucedida e retornou Http 201 Created.
            responseCriar.StatusCode.Should().Be(HttpStatusCode.Created);

            //Arrange
            //Criando dados da requisi��o para autenticar o usu�rio
            var requestAutenticar = new AutenticarUsuarioRequest(
                Email: criarRequest.Email,
                Senha: criarRequest.Senha
                );

            //Act
            //Enviar requisi��o para o endpoint de autentica��o de usu�rio da API
            var responseAutenticar = _client.PostAsJsonAsync("/api/usuario/autenticar", requestAutenticar).Result;

            //Assert
            //Verificar se a resposta foi bem sucedida e retornou Http 200 OK.
            responseAutenticar.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact(
            DisplayName = "Deve retornar acesso negado para usu�rio inv�lido."
        )]
        public void DeveRetornarAcessoNegadoParaUsuarioInvalido()
        {
            //Arrange
            //Criando dados da requisi��o para autenticar o usu�rio
            var request = new AutenticarUsuarioRequest(
                Email: _Faker.Internet.Email(), //Email inv�lido
                Senha: "@AcessoNegado2025"
                );

            //Act
            //Enviar requisi��o para o endpoint de autentica��o de usu�rio da API
            var response = _client.PostAsJsonAsync("/api/usuario/autenticar", request).Result;

            //Assert
            //Verificar se a resposta retornou Http 401 Unauthorized.
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            //Assert
            //Verificar se a mensagem de erro est� correta
            var content = response.Content.ReadAsStringAsync().Result;
            content.Should().Contain("Usu�rio ou senha inv�lidos.");
        }
    }
}
