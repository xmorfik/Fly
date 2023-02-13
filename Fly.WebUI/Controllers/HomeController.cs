using AutoMapper;
using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
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

    public HomeController(
        ILogger<HomeController> logger,
        IService<Flight, FlightParameter> service,
        IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
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
        ViewData["result"] = new List<Flight>();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(FlightParameterViewModel parameters)
    {
        var par = _mapper.Map<FlightParameter>(parameters);
        var result = await _service.GetListAsync(par, new Page() { PageNumber = parameters.PageNumber, PageSize = parameters.PageSize });
        ViewData["result"] = result;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Result(PagedResponse<ICollection<Flight>> response)
    {
        return View(response);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
