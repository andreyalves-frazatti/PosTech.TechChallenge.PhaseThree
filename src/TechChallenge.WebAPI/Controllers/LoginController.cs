using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Domain.Repositories;
using TechChallenge.WebAPI.Models;
using TechChallenge.WebAPI.Security;

namespace TechChallenge.WebAPI.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    public LoginController(IUserRepository userRepository, SecurityService securityService)
    {
        _userRepository = userRepository;
        _securityService = securityService;
    }

    private readonly IUserRepository _userRepository;
    private readonly SecurityService _securityService;

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Endpoint that enables access token generation.")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AuthenticateAsync([FromBody] CredentialsModel model,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(model.Username!, model.Password!, cancellationToken);

        return user is null
            ? Unauthorized()
            : Ok(_securityService.GenerateToken(user));
    }
}