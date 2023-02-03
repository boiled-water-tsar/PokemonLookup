using Newtonsoft.Json;

namespace PokeLookup;

[JsonObject(MemberSerialization.OptIn)]
public class RawPokemon
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("flavor_text_entries")]
    public IEnumerable<FlavorTextEntry> FlavorTextEntries { get; set; }
}

[JsonObject]
public class FlavorTextEntry
{
    [JsonProperty("flavor_text")]
    public string flavorText { get; set; }
    
    public Content Version { get; set; }
    public Content Language { get; set; }
}

[JsonObject]
public class Content
{
    public string Name { get; set; }
    
    public string Url { get; set; }
}
