using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NSubstitute;
using PokeLookup;
using Xunit;

namespace PokeTest;

public class PokeController_Should : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PokeController_Should(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_ById_ReturnsFormattedPokemon()
    {
        // Arrange
        var id = 1;
        var client = _factory.CreateClient();
        var _pokemonService = Substitute.For<IPokemonService>();
        var bulbasaur = new ShakespeareanPokemon
        {
            Name = "Bulbasaur",
            Description = "A strange seed wast planted on its back at birth. The plant sprouts and grows with this pokémon."
        };

        _pokemonService.GetPokemon(1).Returns(Task.FromResult(bulbasaur));

        // Act
        var response = await client.GetAsync($"/Poke/{id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<ShakespeareanPokemon>(content);

        Assert.IsType<ShakespeareanPokemon>(deserialized);
        Assert.Equal(bulbasaur.Name, deserialized.Name);
        Assert.Equal(bulbasaur.Description, deserialized.Description);
    }
    
    [Fact]
    public async Task Get_ByName_ReturnsFormattedPokemon()
    {
        // Arrange
        var client = _factory.CreateClient();
        var _pokemonService = Substitute.For<IPokemonService>();
        var bulbasaur = new ShakespeareanPokemon
        {
            Name = "Bulbasaur",
            Description = "A strange seed wast planted on its back at birth. The plant sprouts and grows with this pokémon."
        };

        _pokemonService.GetPokemon(1).Returns(Task.FromResult(bulbasaur));

        // Act
        var response = await client.GetAsync($"/Poke/{bulbasaur.Name}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<ShakespeareanPokemon>(content);

        Assert.IsType<ShakespeareanPokemon>(deserialized);
        Assert.Equal(bulbasaur.Name, deserialized.Name);
        Assert.Equal(bulbasaur.Description, deserialized.Description);
    }
}