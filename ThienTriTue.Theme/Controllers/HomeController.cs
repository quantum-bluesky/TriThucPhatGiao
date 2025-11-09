// ThienTriTue.Theme/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;

namespace ThienTriTue.Theme.Controllers;

[Feature("ThienTriTue.Theme")]
public class HomeController : Controller
{
    // Dùng root ~/Views để phù hợp với view lookup của Razor/OrchardCore (embedded/compiled views)
    private const string ThemeRoot = "~/Views";

    [HttpGet("/")]
    public IActionResult Index() => View($"{ThemeRoot}/Home/Index.cshtml");

    [HttpGet("/giao-duc")]
    public IActionResult Education() => View($"{ThemeRoot}/Education/Index.cshtml");

    [HttpGet("/phat-giao")]
    public IActionResult Budha() => View($"{ThemeRoot}/Buddha/Index.cshtml");

    [HttpGet("/nhan-qua")]
    public IActionResult Morals() => View($"{ThemeRoot}/Morals/Index.cshtml");
}