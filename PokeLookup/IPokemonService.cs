namespace PokeLookup;

public interface IPokemonService
{
    Task<ShakespeareanPokemon> GetPokemon(int id);
    
    Task<ShakespeareanPokemon> GetPokemon(string id);
}