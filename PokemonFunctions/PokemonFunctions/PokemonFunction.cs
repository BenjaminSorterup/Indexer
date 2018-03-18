using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using PokemonFunctions.Methods;

namespace PokemonFunctions
{
    public static class PokemonFunction
    {
        [FunctionName("IndexFirstGenerationPokemon")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");
                var firstGen = await Generations.GetFirstGenerationAsync(log);
                IndexService.IndexCollection(firstGen, "firstgeneration");
                return req.CreateResponse(HttpStatusCode.OK, "Indexing completed!");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return req.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
