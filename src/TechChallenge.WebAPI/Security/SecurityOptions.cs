namespace TechChallenge.WebAPI.Security;

public class SecurityOptions
{
    public const string OptionSection = "Security";

    public string SecretKey { get; set; } = string.Empty;
}