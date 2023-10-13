using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Commands.CreateNews;

namespace TechChallenge.WebAPI.Models;

public class CreateNewsModel
{
    [Required(AllowEmptyStrings = false)]
    [SwaggerSchema(Description = "Título da notícia.")]
    public string? Title { get; set; }

    [Required(AllowEmptyStrings = false)]
    [SwaggerSchema(Description = "Conteúdo da notícia.")]
    public string? Content { get; set; }

    [Required(AllowEmptyStrings = false)]
    [SwaggerSchema(Description = "Autor responsável pela publicação.")]
    public string? Author { get; set; }

    public CreateNewsCommand MapToCommand()
    {
        return new CreateNewsCommand(Title!, Content!, DateTime.Now, Author!);
    }
}