using Microsoft.Azure.WebJobs.Host;
using PokemonFunctions.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace PokemonFunctions.Methods
{
    public static class Generations
    {
        public const int PokemonAmountFirstGen = 151;
        public const int PokemonAmountSecondGen = 100;
        public const int PokemonAmountThirdGen = 135;
        private static HttpClient client = new HttpClient();

        public static async Task<List<Pokemon>> GetPokemonSampleAsync(int count, TraceWriter log)
        {
            var start = 1;
            return await GetPokemonsAsync(start, count, log);
        }

        public static async Task<List<Pokemon>> GetFirstGenerationAsync(TraceWriter log)
        {
            var start = 1;
            return await GetPokemonsAsync(start, PokemonAmountFirstGen, log);
        }

        public static async Task<List<Pokemon>> GetSecondGenerationAsync(TraceWriter log)
        {
            var start = PokemonAmountFirstGen + 1;
            return await GetPokemonsAsync(start, PokemonAmountSecondGen, log);
        }

        public static async Task<List<Pokemon>> GetThirdGenerationAsync(TraceWriter log)
        {
            var start = PokemonAmountFirstGen + PokemonAmountSecondGen + 1;
            return await GetPokemonsAsync(start, PokemonAmountThirdGen, log);
        }

        private static async Task<List<Pokemon>> GetPokemonsAsync(int start, int count, TraceWriter log)
        {
            Stopwatch totalTimeStopwatch = new Stopwatch();
            var pokemons = new List<Pokemon>();
            totalTimeStopwatch.Start();
            foreach (var index in Enumerable.Range(start, count))
            {
                Stopwatch individualTimeStopwatch = new Stopwatch();
                individualTimeStopwatch.Start();
                var pokemon = await GetPokemonAsync(index);
                pokemon = FormatPokemonName(pokemon);
                log.Info($"{pokemon.Name} {index}/{count}");
                pokemons.Add(pokemon);
                individualTimeStopwatch.Stop();
                log.LogTime(individualTimeStopwatch.Elapsed);
            }
            totalTimeStopwatch.Stop();
            log.LogTime(totalTimeStopwatch.Elapsed);
            return pokemons;
        }

        private static async Task<Pokemon> GetPokemonAsync(int index)
        {
            string pokemonApiBaseUri = ConfigurationManager.AppSettings["PokemonApi"];
            string requestUri = $"{pokemonApiBaseUri}/pokemon/{index}/ ";
            Pokemon pokemon = null;
            HttpResponseMessage response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
                pokemon = await response.Content.ReadAsAsync<Pokemon>();
            return pokemon;
        }

        private static Pokemon FormatPokemonName(Pokemon pokemon)
        {
            var name = pokemon.Name;
            if(name != null && name.Length > 0)
            {
                var firstLetter = name.First().ToString().ToUpper();
                var rest = string.Empty;
                if(name.Length > 1)
                {
                    rest = name.Substring(1);
                }
                name = $"{firstLetter}{rest}";
                pokemon.Name = name;
            }
            return pokemon;
        }
    }
}