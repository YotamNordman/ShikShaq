using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShikShaq.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<String> Scores()
        {
            // TODO: use this only before showing menachem the project... 
            // Since yariv may be paying for that

            //var client = new RestClient($"https://api-football-v1.p.rapidapi.com/v2/fixtures/league/524/last/100");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            //request.AddHeader("x-rapidapi-key", "3c5d9d988amsh4aa8980944ef4f2p118e80jsn3713bac3c462");
            //request.AddHeader("useQueryString", "true");

            //IRestResponse response = await client.ExecuteAsync(request);
            //return response.Content.ToString();

            String scoresResult = "";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("Data/ExampleGamesResponse.json"))
                {
                    // Read the stream to a string, and write the string to the console.
                    scoresResult = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return scoresResult;
        }
    }
}
