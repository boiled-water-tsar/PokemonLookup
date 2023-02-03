using Newtonsoft.Json;

namespace PokeLookup;

[JsonObject(MemberSerialization.OptIn)]
public class ShakespeareResponse
{
    [JsonProperty("contents")]
    public Contents Contents { get; set; }
}

[JsonObject]
public class Contents
{
    public string translated { get; set; }
    
    public string text { get; set; }
    
    public string translation { get; set; }
}
