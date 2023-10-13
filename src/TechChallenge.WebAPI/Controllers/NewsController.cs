using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Queries;
using TechChallenge.Domain.Entities;
using TechChallenge.WebAPI.Models;

namespace TechChallenge.WebAPI.Controllers;

[Route("api/news")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [SwaggerOperation(Summary = "Endpoint that allows the registration of news.")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddAsync([FromBody] CreateNewsModel model, CancellationToken cancellationToken)
    {
        var news = await _mediator.Send(model.MapToCommand(), cancellationToken);
        return Ok(news);
    }

    [HttpGet]
    [Authorize(Roles = "admin,reader")]
    [SwaggerOperation(Summary = "Endpoint that makes it possible to get all the news.")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<News>))]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllAsync([FromServices] NewsQueries newsQueries,
        CancellationToken cancellationToken)
    {
        var news = await newsQueries.GetAllAsync(cancellationToken);

        return news.Any()
            ? Ok(news)
            : NoContent();
    }
}