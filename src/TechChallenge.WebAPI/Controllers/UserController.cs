using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Commands.CreateUser;

namespace TechChallenge.WebAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Endpoint that allows the registration of user.")]
    [SwaggerResponse(StatusCodes.Status201Created)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAsync([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _mediator.Send(command, cancellationToken);

            return new ObjectResult(user) {StatusCode = StatusCodes.Status201Created};
        }
        catch (Exception e)
        {
            return new ObjectResult(e.Message) {StatusCode = StatusCodes.Status500InternalServerError};
        }
    }
}