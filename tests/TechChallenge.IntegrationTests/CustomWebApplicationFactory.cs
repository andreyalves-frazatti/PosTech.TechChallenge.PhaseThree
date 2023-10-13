using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application.DependecyInjections;
using TechChallenge.Infrastructure;
using TechChallenge.Infrastructure.DependecyInjections;
using TechChallenge.WebAPI.Security;

namespace TechChallenge.IntegrationTests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private const string TestEnvironment = "Tests";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(TestEnvironment);

        builder.ConfigureServices(services =>
        {
            builder.ConfigureAppConfiguration((_, configurator) =>
            {
                var configuration = configurator
                    .AddJsonFile($"appsettings.{TestEnvironment}.json")
                    .AddEnvironmentVariables()
                    .Build();

                var sqlConnectionString = configuration.GetConnectionString("SqlConnectionString");

                services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(sqlConnectionString));
                services.AddSecurity(configuration);
                services.AddRepositories();
                services.AddCommands();
                services.AddQueries();

                services.AddControllers();
                services.AddEndpointsApiExplorer();
            });
        });
    }
}