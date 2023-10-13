using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.Commands.CreateUser;
using TechChallenge.Infrastructure;

namespace TechChallenge.IntegrationTests.Fixtures;

public static class UserTestFixture
{
    public static bool ExistsOnDatabase(
        this CreateUserCommand command,
        CustomWebApplicationFactory<Program> webApplicationFactory)
    {
        using var scope = webApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        return context
            .Users!
            .Any(user
                => user.Username == command.Username
                   && user.Password == command.Password
                   && user.Role == command.Role);
    }
}