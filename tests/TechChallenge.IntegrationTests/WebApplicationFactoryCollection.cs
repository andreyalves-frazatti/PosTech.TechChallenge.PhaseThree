namespace TechChallenge.IntegrationTests;

[CollectionDefinition("WebApplicationFactory")]
public class WebApplicationFactoryCollection 
    : ICollectionFixture<CustomWebApplicationFactory<Program>>
{
}