using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fly.WebUI.Controllers;

[Authorize(Roles = "Administrator")]
public class ManagersController : Controller
{
    private readonly IService<Manager, ManagerParameter> _service;
    private readonly AuthorityUri _authorityUri;

    public ManagersController(
        IService<Manager, ManagerParameter> service,
        IOptions<AuthorityUri> options)
    {
        _service = service;
        _authorityUri = options.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool isSelect, string redirectUri)
    {
        var managerViewModel = new ManagersViewModel();
        var response = await _service.GetListAsync(new ManagerParameter(), new Page());
        managerViewModel.PagedResponse = response;
        managerViewModel.MetaData = response.MetaData;

        return View(managerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ManagersViewModel managerViewModel)
    {
        var response = await _service.GetListAsync(managerViewModel.ManagerParameter, managerViewModel.MetaData.ToPage());
        managerViewModel.PagedResponse = response;
        managerViewModel.MetaData = response.MetaData;

        return View(managerViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return Redirect(_authorityUri.Uri + "account/register/manager");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.GetAsync(id);
        return View(item.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Manager item)
    {
        await _service.DeleteAsync(item.Id ?? 0);
        return RedirectToAction("index");
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Manager item)
    {
        await _service.UpdateAsync(item);
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Select(int id, string redirectUri)
    {
        Response.Cookies.Append("SelectedManagerId", id.ToString());
        return Redirect(redirectUri);
    }
}
