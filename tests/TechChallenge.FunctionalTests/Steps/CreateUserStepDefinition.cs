using FluentAssertions;
using Moq;
using TechChallenge.Application.Commands.CreateUser;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.FunctionalTests.Steps;

[Binding]
public sealed class CreateUserStepDefinition
{
    private Mock<IUserRepository>? _mockUserRepository;
    private CreateUserCommandHandler? _createUserCommandHandler;

    private CreateUserCommand? _createUserCommand;
    private CancellationToken _cancellationToken;

    [Given(@"que um usuário precise ser cadastrado")]
    public void GivenQueUmUsuarioPreciseSerCadastrado()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _cancellationToken = new CancellationToken();
    }

    [Given(@"os seguintes parâmetros válidos sejam informados:")]
    public void GivenOsSeguintesParametrosValidosSejamInformados(Table table)
    {
        var values = table.Rows.FirstOrDefault();

        var username = values!["Username"];
        var password = values["Password"];
        var role = values["Role"];

        _createUserCommand = new CreateUserCommand(username, password, role);
    }

    [Given(@"o mesmo não exista")]
    public void GivenOMesmoNaoExista()
    {
        _mockUserRepository!
            .Setup(c => c.AlreadyExistsAsync(
                It.Is<User>(user
                    => user.Username == _createUserCommand!.Username
                       && user.Password == _createUserCommand!.Password
                       && user.Role == _createUserCommand!.Role),
                _cancellationToken))
            .ReturnsAsync(false);
    }


    [When(@"solicitado o cadastro")]
    public void WhenSolicitadoOCadastroDoMesmo()
    {
        _createUserCommandHandler = new CreateUserCommandHandler(_mockUserRepository!.Object);
    }

    [Then(@"o usuário deve ser cadastrado com sucesso")]
    public async Task DadoOUsuarioDeveSerCadastradoComSucesso()
    {
        var user = await _createUserCommandHandler!.Handle(_createUserCommand!, _cancellationToken);

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Username.Should().Be(_createUserCommand!.Username);
        user.Password.Should().Be(_createUserCommand!.Password);
        user.Role.Should().Be(_createUserCommand!.Role);

        _mockUserRepository!.Verify(c => c.AddAsync(user, _cancellationToken), Times.Once);
    }

    [Given(@"o mesmo já exista")]
    public void GivenOMesmoJaExista()
    {
        _mockUserRepository!
            .Setup(c => c.AlreadyExistsAsync(
                It.Is<User>(user
                    => user.Username == _createUserCommand!.Username
                       && user.Password == _createUserCommand!.Password
                       && user.Role == _createUserCommand!.Role),
                _cancellationToken))
            .ReturnsAsync(true);
    }

    [Then(@"deve retornar um erro com a seguinte mensagem: ""(.*)""")]
    public async Task ThenDeveRetornarUmErroComASeguinteMensagem(string message)
    {
        Func<Task> act = () => _createUserCommandHandler!.Handle(_createUserCommand!, _cancellationToken);

        await act.Should()
            .ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage(message);

        _mockUserRepository!
            .Verify(c => c.AddAsync(
                It.IsAny<User>(),
                It.IsAny<CancellationToken>()), Times.Never);
    }
}