using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace TechChallenge.WebAPI.Models;

public class CredentialsModel
{
    [Required(AllowEmptyStrings = false)]
    [SwaggerSchema(Description = "Nome de usuário.")]
    public string? Username { get; set; }

    [Required(AllowEmptyStrings = false)]
    [SwaggerSchema(Description = "Senha de acesso.")]
    public string? Password { get; set; }
}