using Microsoft.EntityFrameworkCore;
using TechChallenge.Application.DependecyInjections;
using TechChallenge.Infrastructure;
using TechChallenge.Infrastructure.DependecyInjections;
using TechChallenge.WebAPI.Security;
using TechChallenge.WebAPI.Swagger;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder
    .Configuration
    .GetConnectionString("SqlConnectionString");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddCommands();
builder.Services.AddQueries();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ConfigureSwaggerDoc();
    c.EnableAnnotations();
    c.ConfigureSwaggerSecurityScheme();
});

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public abstract partial class Program
{
}