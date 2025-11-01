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
           DisplayName = "Não deve permitir criar usuários com o mesmo email."
       )]
        public void NaoDevePermitirCriarUsuariosComMesmoEmail()
        {
            //Arrange
            //Criando dados da requisição
            var request = new CriarContaRequest(
                Nome: _Faker.Name.FullName(),
                Email: _Faker.Internet.Email(),
                Senha: "@Teste2025"
                );

            //Act
            //Enviar requisição para o endpoint de criação de cadastro de usuario da API
            var response1 = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;
            response1.StatusCode.Should().Be(HttpStatusCode.Created);

            //Act
            //Tentar criar o mesmo usuário novamente
            var response2 = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Assert
            //Verificar se a resposta retornou Http 400 Bad Request
            response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //Assert
            //Verificar se a mensagem de erro está correta
            var content = response2.Content.ReadAsStringAsync().Result;
            content.Should().Contain("O email informado já está em uso.");


        }

        [Fact(
           DisplayName = "Deve obrigar o preenchimento de senha forte."
       )]
        public void DeveObrigarPreenchimentoDeSenhaForte()
        {
            //Arrange
            //Criando dados da requisição com senha fraca
            var request = new CriarContaRequest(
                Nome: _Faker.Person.FullName,
                Email: _Faker.Internet.Email(),
                Senha: "123"
                );

            //Act
            //Enviar requisição para o endpoint de criação de cadastro de usuario da API
            var response = _client.PostAsJsonAsync("/api/usuario/criar", request).Result;

            //Verificar se a mensagem de erro está correta
            var content = response.Content.ReadAsStringAsync().Result;
            content.Should().Contain("A senha deve ter pelo menos 1 letra minúscula," +
                " 1 letra maiúscula, 1 número, 1 símbolo e no mínimo 8 caracteres.");
        }

        [Fact(
           DisplayName = "Deve autenticar um usuário com sucesso."
       )]
        public void DeveAutenticarUsuarioComSucesso()
        {
            //Arrange
            //Criando dados da requisição para criar o usuário
                var criarRequest = new CriarContaRequest(
                Nome: _Faker.Person.FullName,
                Email: _Faker.Internet.Email(),
                Senha: "@Teste2025"
                );
            //Act
            //Enviar requisição para o endpoint de criação de cadastro de usuario da API
            var responseCriar = _client.PostAsJsonAsync("/api/usuario/criar", criarRequest).Result;

            //Assert
            //Verificar se a resposta foi bem sucedida e retornou Http 201 Created.
            responseCriar.StatusCode.Should().Be(HttpStatusCode.Created);

            //Arrange
            //Criando dados da requisição para autenticar o usuário
            var requestAutenticar = new AutenticarUsuarioRequest(
                Email: criarRequest.Email,
                Senha: criarRequest.Senha
                );

            //Act
            //Enviar requisição para o endpoint de autenticação de usuário da API
            var responseAutenticar = _client.PostAsJsonAsync("/api/usuario/autenticar", requestAutenticar).Result;

            //Assert
            //Verificar se a resposta foi bem sucedida e retornou Http 200 OK.
            responseAutenticar.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact(
            DisplayName = "Deve retornar acesso negado para usuário inválido."
        )]
        public void DeveRetornarAcessoNegadoParaUsuarioInvalido()
        {
            //Arrange
            //Criando dados da requisição para autenticar o usuário
            var request = new AutenticarUsuarioRequest(
                Email: _Faker.Internet.Email(), //Email inválido
                Senha: "@AcessoNegado2025"
                );

            //Act
            //Enviar requisição para o endpoint de autenticação de usuário da API
            var response = _client.PostAsJsonAsync("/api/usuario/autenticar", request).Result;

            //Assert
            //Verificar se a resposta retornou Http 401 Unauthorized.
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            //Assert
            //Verificar se a mensagem de erro está correta
            var content = response.Content.ReadAsStringAsync().Result;
            content.Should().Contain("Usuário ou senha inválidos.");
        }
    }
}
