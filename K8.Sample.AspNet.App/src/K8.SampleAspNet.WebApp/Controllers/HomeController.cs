using K8.SampleAspNet.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace K8.SampleAspNet.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string weatherDetails = GetWeatherDetails().GetAwaiter().GetResult();

            ViewBag.WeatherDetails = weatherDetails;

            return View();
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

        public async Task<string> GetWeatherDetails()
        {

            string apiResponse = String.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://k8-backendapi-cluster-service:6002/weatherforecast"))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse = ex.ToString();
            }
            return apiResponse;
        }
    }
}