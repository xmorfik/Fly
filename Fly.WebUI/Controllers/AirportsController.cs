using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class AirportsController : Controller
{
    private readonly IService<Airport, AirportParameter> _service;
    private readonly IService<City, CityParameter> _cities;

    public AirportsController(
        IService<Airport, AirportParameter> service,
        IService<City, CityParameter> cities)
    {
        _service = service;
        _cities = cities;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool isSelect, string redirectUri)
    {
        var airportViewModel = new AirportsViewModel();
        var response = await _service.GetListAsync(new AirportParameter(), new Page());

        airportViewModel.PagedResponse = response;
        airportViewModel.MetaData = response.MetaData;
        airportViewModel.RedirectUri = redirectUri;
        airportViewModel.IsSelect = isSelect;

        return View(airportViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        int cityId;

        if (int.TryParse(Request.Cookies["SelectedCityId"], out cityId))
        {
            ViewData["SelectedCityId"] = cityId;
            var result = await _cities.GetAsync(cityId);
            ViewData["SelectedCity"] = result.Data;
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }


    [HttpPost]
    public async Task<IActionResult> Index(AirportsViewModel airportViewModel)
    {
        var response = await _service.GetListAsync(airportViewModel.AirportParameter, airportViewModel.MetaData.ToPage());
        airportViewModel.PagedResponse = response;
        airportViewModel.MetaData = response.MetaData;
        return View(airportViewModel);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Airport item)
    {
        Response.Cookies.Delete("SelectedCityId");

        await _service.CreateAsync(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Airport item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Airport item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Select(int id, string redirectUri)
    {
        Response.Cookies.Append("SelectedAirportId", id.ToString());
        return Redirect(redirectUri);
    }
}
