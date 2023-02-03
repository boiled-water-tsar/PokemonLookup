using LazyCache;
using Newtonsoft.Json;

namespace PokeLookup;

public class PokemonService : IPokemonService
{
    private readonly HttpClient _httpClient = new ();
    private readonly IAppCache _cache = new CachingService();

    public async Task<ShakespeareanPokemon> GetPokemon(int id)
    {
        var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<RawPokemon>(content);

        return await TransformPokemon(deserialized);
    }
    
    public async Task<ShakespeareanPokemon> GetPokemon(string name)
    {
        var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{name.ToLower()}");
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<RawPokemon>(content);

        return await TransformPokemon(deserialized);
    }

    private async Task<ShakespeareanPokemon> TransformPokemon(RawPokemon rawPokemon)
    {
        if (_cache.TryGetValue(rawPokemon.Id, out ShakespeareanPokemon shakespeareanPokemonFromCache))
        {
            return shakespeareanPokemonFromCache;
        }
        
        var rawDescription = FilterDescriptions(rawPokemon);
        var shakespeareanDescription = await TransformDescription(rawDescription);
        
        var shakespeareanPokemon = new ShakespeareanPokemon
        {
            Name = rawPokemon.Name,
            Description = shakespeareanDescription,
        };

        _cache.Add(rawPokemon.Id, shakespeareanPokemon);
        
        return shakespeareanPokemon;
    }

    private async Task<string> TransformDescription(string description)
    {
        var data = new FormUrlEncodedContent(new Dictionary<string, string> {{ "text", description }});
        
        // TODO: Some kind of handling here for too many requests
        var response = await _httpClient.PostAsync("https://api.funtranslations.com/translate/shakespeare.json", data);
        var content = await response.Content.ReadAsStringAsync();
        var deserialized = JsonConvert.DeserializeObject<ShakespeareResponse>(content);

        return deserialized.Contents.translated;
    }

    private static string FilterDescriptions(RawPokemon rawPokemon)
    {
        return rawPokemon.FlavorTextEntries.First(r => r.Language.Name == "en").flavorText;
    }
}