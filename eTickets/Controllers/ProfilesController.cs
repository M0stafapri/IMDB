using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProfilesController : Controller
    {
        private readonly IProfilesService _service;

        public ProfilesController(IProfilesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProfiles = await _service.GetAllAsync();
            return View(allProfiles);
        }


        //Get: Profiles/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")]Profile profile)
        {
            if (!ModelState.IsValid) return View(profile);
            await _service.AddAsync(profile);
            return RedirectToAction(nameof(Index));
        }

        //Get: Profiles/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var profileDetails = await _service.GetByIdAsync(id);
            if (profileDetails == null) return View("NotFound");
            return View(profileDetails);
        }

        //Get: Profiles/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var profileDetails = await _service.GetByIdAsync(id);
            if (profileDetails == null) return View("NotFound");
            return View(profileDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Profile profile)
        {
            if (!ModelState.IsValid) return View(profile);
            await _service.UpdateAsync(id, profile);
            return RedirectToAction(nameof(Index));
        }

        //Get: Profiles/Delete/1

        public async Task<IActionResult> Delete(int id)
        {
            var profileDetails = await _service.GetByIdAsync(id);
            if (profileDetails == null) return View("NotFound");
            return View(profileDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var profileDetails = await _service.GetByIdAsync(id);
            if (profileDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
