using Newtonsoft.Json;

namespace PokemonFunctions.Models
{
    public class Sprites
    {
        [JsonProperty("front_default")]
        public string DefaultSprite { get; set; }
    }
}
