﻿using Fly.Core.Entities;
using Fly.Core.Pagination;
using Fly.Core.Parameters;
using Fly.Core.Services;
using Fly.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fly.WebUI.Controllers
{
    public class ManagersController : Controller
    {
        private readonly IService<Manager, ManagerParameter> _service;

        public ManagersController(IService<Manager, ManagerParameter> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var managerViewModel = new ManagersViewModel();
            var response = await _service.GetListAsync(new ManagerParameter(), new Page());
            managerViewModel.PagedResponse = response;
            managerViewModel.MetaData = response.MetaData;
            return View(managerViewModel);
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
        public async Task<IActionResult> Index(ManagersViewModel managerViewModel)
        {
            var response = await _service.GetListAsync(managerViewModel.ManagerParameter, managerViewModel.MetaData.ToPage());
            managerViewModel.PagedResponse = response;
            managerViewModel.MetaData = response.MetaData;
            return View(managerViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Manager item)
        {
            await _service.CreateAsync(item);
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Manager item)
        {
            await _service.DeleteAsync(item.Id ?? 0);
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Manager item)
        {
            await _service.UpdateAsync(item);
            return View(item);
        }
    }
}