using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoEx.Data;
using DemoEx.Models;

namespace DemoEx.Controllers
{
    public class Applications1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Applications1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applications1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applications.Include(a => a.TypeEquipment).Include(a => a.TypeProblem).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Applications1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.TypeEquipment)
                .Include(a => a.TypeProblem)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.IdApplication == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications1/Create
        public IActionResult Create()
        {
            ViewData["IdTypeEquipment"] = new SelectList(_context.TypeEquipments, "IdTypeEquipment", "IdTypeEquipment");
            ViewData["IdTypeProblem"] = new SelectList(_context.TypeProblem, "IdTypeProblem", "IdTypeProblem");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Applications1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdApplication,DateAddition,NameEquipment,IdTypeProblem,Comment,Status,NameClient,Cost,DateEnd,TimeWork,UserId,WorkStatus,PeriodExecution,IdTypeEquipment,Number,Description")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTypeEquipment"] = new SelectList(_context.TypeEquipments, "IdTypeEquipment", "IdTypeEquipment", application.IdTypeEquipment);
            ViewData["IdTypeProblem"] = new SelectList(_context.TypeProblem, "IdTypeProblem", "IdTypeProblem", application.IdTypeProblem);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", application.UserId);
            return View(application);
        }

        // GET: Applications1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["IdTypeEquipment"] = new SelectList(_context.TypeEquipments, "IdTypeEquipment", "IdTypeEquipment", application.IdTypeEquipment);
            ViewData["IdTypeProblem"] = new SelectList(_context.TypeProblem, "IdTypeProblem", "IdTypeProblem", application.IdTypeProblem);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", application.UserId);
            return View(application);
        }

        // POST: Applications1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdApplication,DateAddition,NameEquipment,IdTypeProblem,Comment,Status,NameClient,Cost,DateEnd,TimeWork,UserId,WorkStatus,PeriodExecution,IdTypeEquipment,Number,Description")] Application application)
        {
            if (id != application.IdApplication)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.IdApplication))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTypeEquipment"] = new SelectList(_context.TypeEquipments, "IdTypeEquipment", "IdTypeEquipment", application.IdTypeEquipment);
            ViewData["IdTypeProblem"] = new SelectList(_context.TypeProblem, "IdTypeProblem", "IdTypeProblem", application.IdTypeProblem);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", application.UserId);
            return View(application);
        }

        // GET: Applications1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.TypeEquipment)
                .Include(a => a.TypeProblem)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.IdApplication == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.IdApplication == id);
        }
    }
}
