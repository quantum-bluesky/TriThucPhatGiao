// ThienTriTue.Theme/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;

namespace ThienTriTue.Theme.Controllers;

[Feature("ThienTriTue.Theme")]
public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View("~/Views/Home/Index.cshtml");
    }

    [HttpGet("/giao-duc")]
    public IActionResult Education()
    {
        return View("~/Views/Education/Index.cshtml");
    }
}
