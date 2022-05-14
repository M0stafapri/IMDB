using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CommentsController : Controller
    {
        private readonly ICommentService _service;
        private readonly AppDbContext _context;
        public CommentsController(ICommentService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allComments = await _service.GetAllAsync();
            return View(allComments);
        }


        //Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(NewCommentVM comment)
        {
            if (!ModelState.IsValid)
            {

                return View(comment);
            }
            var comments = _context.Comments.ToList();
            var NewId = comments.Count + 1;
            comment.Id = NewId;
            await _service.AddNewCommentAsync(comment);
            return RedirectToAction("Index");
        }

        //Get: Cinemas/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var commentDetails = await _service.GetByIdAsync(id);
            if (commentDetails == null) return View("NotFound");
            return View(commentDetails);
        }

        //Get: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var commentDetails = await _service.GetByIdAsync(id);
            if (commentDetails == null) return View("NotFound");
            return View(commentDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Body")] Comment comment)
        {
            if (!ModelState.IsValid) return View(comment);
            await _service.UpdateAsync(id, comment);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var commentDetails = await _service.GetByIdAsync(id);
            if (commentDetails == null) return View("NotFound");
            return View(commentDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var commentDetails = await _service.GetByIdAsync(id);
            if (commentDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
