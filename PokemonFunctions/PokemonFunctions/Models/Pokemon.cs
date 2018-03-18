using Newtonsoft.Json;
using PokemonFunctions.Models.Interfaces;

namespace PokemonFunctions.Models
{
    public class Pokemon : IIndexable
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }
    }
}
