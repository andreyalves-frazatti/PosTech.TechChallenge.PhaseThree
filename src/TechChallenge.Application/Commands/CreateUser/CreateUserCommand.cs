using MediatR;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Commands.CreateUser;

public class CreateUserCommand : IRequest<User>
{
    public CreateUserCommand(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }

    public string Username { get; init; }

    public string Password { get; init; }
    
    public string Role { get; init; }
}