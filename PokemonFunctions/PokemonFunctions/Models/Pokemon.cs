using Newtonsoft.Json;

namespace PokemonFunctions.Models
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }
    }
}
