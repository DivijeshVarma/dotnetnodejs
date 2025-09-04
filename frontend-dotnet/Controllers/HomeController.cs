using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontend_dotnet.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace frontend_dotnet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
    }

    public async Task<IActionResult> Index()
    {
        string message = "No response";
        try
        {
            var response = await _httpClient.GetAsync("http://0.0.0.0:3000/api/message");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(jsonString);
            message = json["message"]?.ToString() ?? "No message";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to call backend");
            message = "Error connecting to backend";
        }

        ViewBag.Message = message;
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
}

