using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers;

public class CitiesController : Controller
{
    private readonly IService<City, CityParameter> _service;

    public CitiesController(IService<City, CityParameter> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool isSelect, string redirectUri)
    {
        var cityViewModel = new CitiesViewModel();
        var response = await _service.GetListAsync(new CityParameter(), new Page());

        cityViewModel.PagedResponse = response;
        cityViewModel.MetaData = response.MetaData;
        cityViewModel.IsSelect = isSelect;
        cityViewModel.RedirectUri = redirectUri;

        return View(cityViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
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
    public async Task<IActionResult> Index(CitiesViewModel cityViewModel)
    {
        var response = await _service.GetListAsync(cityViewModel.CityParameter, cityViewModel.MetaData.ToPage());
        cityViewModel.PagedResponse = response;
        cityViewModel.MetaData = response.MetaData;
        return View(cityViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(City item)
    {
        await _service.CreateAsync(item);
        return RedirectToAction("index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(City item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(City item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Select(int id, string redirectUri)
    {
        Response.Cookies.Append("SelectedCityId", id.ToString());
        return Redirect(redirectUri);
    }
}
