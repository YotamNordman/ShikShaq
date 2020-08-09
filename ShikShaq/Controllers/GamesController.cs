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
