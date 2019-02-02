using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProject.Models;

namespace NewProject.Controllers
{
    public class AmbionsController : Controller
    {
        private readonly NewProjectContext _context;

        public AmbionsController(NewProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ambion.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Ambion ambion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(ambion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(ambion);
                }
            }
            catch
            {
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ambion = await _context.Ambion
                .FirstOrDefaultAsync(m => m.AmbionId == id);
            if (ambion == null)
            {
                return NotFound();
            }

            return View(ambion);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ambion = await _context.Ambion.FindAsync(id);
            if (ambion == null)
            {
                return NotFound();
            }
            return View(ambion);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Ambion ambion)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(ambion).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ambion);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ambion = await _context.Ambion
                .FirstOrDefaultAsync(m => m.AmbionId == id);
            if (ambion == null)
            {
                return NotFound();
            }

            return View(ambion);


        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var ambion = await _context.Ambion.FindAsync(id);
                _context.Ambion.Remove(ambion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }


    }
}
