using ChatModels.Models;
using LangChain.Providers.OpenAI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using LangChain;
using LangChain.Providers;

namespace ChatModels.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var apiKey = _configuration["LangChainSettings:ApiKey"];
                using var httpClient = new HttpClient();
                var model = new Gpt4Model(apiKey, httpClient);
                var generatedMessage = await model.GenerateAsync("For your information, your response will be displayed on the home page of my web page, tz7.eu. Please say something nice to our visitor. Also add, that your response is an AI generated message and introduce yourself");

                return View((object)generatedMessage); // Pass the message to the view.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating message from Gpt4Model");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
