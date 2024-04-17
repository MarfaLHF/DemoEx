using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoEx.Data;
using DemoEx.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DemoEx.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;


        public ApplicationsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Applications

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            IQueryable<Application> applicationDbContext;


            if (user != null)
            {
                string ro = user.Role;

                ViewBag.Ro = ro;
                if (ro == "2")
                {
                    applicationDbContext = _context.Applications
                        .Include(a => a.TypeEquipment)
                        .Include(a => a.TypeProblem)
                        .Include(a => a.User);
                }
                else
                {
                    applicationDbContext = _context.Applications
                        .Include(a => a.TypeEquipment)
                        .Include(a => a.TypeProblem)
                        .Include(a => a.User)
                        .Where(a => a.UserId == userId);
                }
                return View(await applicationDbContext.ToListAsync());
            }
            else
                return View();
        }


        // GET: Applications/Details/5
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

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["IdTypeEquipment"] = new SelectList(_context.TypeEquipments, "IdTypeEquipment", "Name");
            ViewData["IdTypeProblem"] = new SelectList(_context.TypeProblem, "IdTypeProblem", "Name");
            return View();
        }

        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdApplication,DateAddition,NameEquipment,IdTypeProblem,Comment,Status,NameClient,Cost,DateEnd,TimeWork,WorkStatus,PeriodExecution,IdTypeEquipment,Number,Description")] Application application)
        {
            Random rnd = new Random(1);
            //if (ModelState.IsValid)
            //{
                // Получаем ID текущего пользователя
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Заполняем UserId текущим идентификатором пользователя
            application.UserId = userId;
            application.DateAddition = DateTime.Now;
            application.Status = "в ожидании";
            application.WorkStatus = "не выполнено";
            application.Number = rnd.Next(1, 500);

            _context.Add(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}

            // Если модель невалидна, добавляем сообщения об ошибках в ModelState
            foreach (var modelStateEntry in ModelState.Values)
            {
                foreach (var error in modelStateEntry.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }

            ViewData["IdTypeEquipment"] = new SelectList(_context.TypeEquipments, "IdTypeEquipment", "IdTypeEquipment", application.IdTypeEquipment);
            ViewData["IdTypeProblem"] = new SelectList(_context.TypeProblem, "IdTypeProblem", "IdTypeProblem", application.IdTypeProblem);

            // Удалите строку, относящуюся к UserId

            return View(application);
        }



        // GET: Applications/Edit/5
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

        // POST: Applications/Edit/5
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

        // GET: Applications/Delete/5
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

        // POST: Applications/Delete/5
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
