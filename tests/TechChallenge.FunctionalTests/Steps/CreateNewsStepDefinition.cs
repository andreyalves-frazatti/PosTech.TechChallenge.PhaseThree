using FluentAssertions;
using Moq;
using TechChallenge.Application.Commands.CreateNews;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.FunctionalTests.Steps;

[Binding]
public sealed class CreateNewsStepDefinition
{
    private Mock<INewsRepository>? _mockNewsRepository;
    private CreateNewsCommandHandler? _createNewsCommandHandler;

    private CreateNewsCommand? _createNewsCommand;
    private CancellationToken _cancellationToken;

    [Given(@"que uma notícia precise ser cadastrada")]
    public void GivenQueUmaNoticiaPreciseSerCadastrada()
    {
        _mockNewsRepository = new Mock<INewsRepository>();
        _cancellationToken = new CancellationToken();
    }

    [Given(@"os seguintes parâmetros válidos da notícia sejam informados:")]
    public void GivenOsSeguintesParametrosValidosDaNoticiaSejamInformados(Table table)
    {
        var values = table.Rows.FirstOrDefault();

        var title = values!["Title"];
        var content = values["Content"];
        var date = DateTime.Parse(values["Date"]);
        var author = values["Author"];

        _createNewsCommand = new CreateNewsCommand(title, content, date, author);
    }

    [When(@"solicitado o cadastro da notícia")]
    public void WhenSolicitadoOCadastroDaNoticia()
    {
        _createNewsCommandHandler = new CreateNewsCommandHandler(_mockNewsRepository!.Object);
    }

    [Then(@"a notícia deve ser cadastrada com sucesso")]
    public async Task ThenANoticiaDeveSerCadastradaComSucesso()
    {
        var news = await _createNewsCommandHandler!.Handle(_createNewsCommand!, _cancellationToken);

        news.Should().NotBeNull();
        news.Id.Should().NotBeEmpty();
        news.Title.Should().Be(_createNewsCommand!.Title);
        news.Content.Should().Be(_createNewsCommand!.Content);
        news.Date.Should().Be(_createNewsCommand!.Date);
        news.Author.Should().Be(_createNewsCommand!.Author);

        _mockNewsRepository!.Verify(c => c.AddAsync(news, _cancellationToken), Times.Once);
    }
}