using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Interfaces;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fly.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IService<Flight, FlightParameter> _service;
    private readonly IMapper _mapper;
    private readonly IApiHttpClientService _httpClientService;

    public HomeController(
        ILogger<HomeController> logger,
        IService<Flight, FlightParameter> service,
        IMapper mapper,
        IApiHttpClientService httpClientService)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
        _httpClientService = httpClientService;
    }

    public async Task<IActionResult> Index(int? id)
    {
        if (id != null)
        {
            var result = await _service.GetAsync(id ?? 0);
            return View(result.Data);
        }
        return View();
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    public IActionResult Search()
    {
        return RedirectToAction("index", "flights", null);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
