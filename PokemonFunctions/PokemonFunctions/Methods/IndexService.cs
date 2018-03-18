using Newtonsoft.Json;
using PokemonFunctions.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonFunctions.Methods
{
    public static class IndexService
    {
        private static HttpClient client = new HttpClient();

        public static void IndexPokemons(List<Pokemon> pokemons, string collection)
        {
            string jsonDocs = JsonConvert.SerializeObject(pokemons);
            string baseSolrUri = ConfigurationManager.AppSettings["SolrUri"];
            string requestUri = $"{baseSolrUri}/{collection}/update/json/docs?commit=true";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = jsonDocs;

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if(httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("HttpRequest was unsuccessful");
                }
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var result = streamReader.ReadToEnd();
                //}
            }
            //HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, jsonDocs);
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new Exception("HttpRequest was unsuccessful");
            //}
        }
    }
}
