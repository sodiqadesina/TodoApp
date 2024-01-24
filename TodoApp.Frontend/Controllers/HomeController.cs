using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TodoApp.Frontend.Models;
using Newtonsoft.Json;

namespace TodoApp.Frontend.Controllers
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://todo-backend.internal.lemonglacier-b76a6f8d.australiaeast.azurecontainerapps.io/Todos");
            var text = await result.Content.ReadAsStringAsync();
            ViewBag.Todos = JsonConvert.DeserializeObject<List<Todo>>(text);
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
}
