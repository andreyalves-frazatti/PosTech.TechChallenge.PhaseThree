using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Infrastructure;
using TechChallenge.WebAPI.Models;

namespace TechChallenge.IntegrationTests.Fixtures;

public static class NewsTestFixture
{
    public static bool ExistsOnDatabase(
        this CreateNewsModel model,
        CustomWebApplicationFactory<Program> webApplicationFactory)
    {
        using var scope = webApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        return context
            .News!
            .Any(news
                => news.Title == model.Title
                   && news.Content == model.Content
                   && news.Author == model.Author);
    }
}