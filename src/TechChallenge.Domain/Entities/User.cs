namespace TechChallenge.Domain.Entities;

public class User
{
    public User(string username, string password, string role)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
        Role = role;
    }

    public Guid Id { get; init; }
    
    public string Username { get; init; }
    
    public string Password { get; init; }
    
    public string Role { get; init; }
}