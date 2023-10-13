using MediatR;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        User user = new(command.Username, command.Password, command.Role);

        if (await _userRepository.AlreadyExistsAsync(user, cancellationToken))
        {
            throw new InvalidOperationException("This user already exists.");
        }

        await _userRepository.AddAsync(user, cancellationToken);

        return user;
    }
}