using Microsoft.AspNetCore.Mvc;

namespace PokeLookup.Controllers;

[ApiController]
[Route("[controller]")]
public class PokeController
{
    private readonly IPokemonService _pokemonService;

    public PokeController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }
    
    [HttpGet("{id:int}", Name = "getById")]
    public async Task<ShakespeareanPokemon> Get(int id)
    {
        return await _pokemonService.GetPokemon(id);
    }
    
    [HttpGet("{name}", Name = "getByName")]
    public async Task<ShakespeareanPokemon> Get(string name)
    {
        return await _pokemonService.GetPokemon(name);
    }
}