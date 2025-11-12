using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGP.Core.Dtos;
using SGP.Core.Entities.Items;
using SGP.Core.Interfaces;
using SGP.Infrastructure.Authentication;
using SGP.Infrastructure.Data;
using SGP.Infrastructure.Persistence;
using SGP.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGP.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemCategoriesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ItemCategoriesController(ApplicationDbContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/ItemCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemCategories.Include(i => i.ParentCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/ItemCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategory = await _context.ItemCategories
                .Include(i => i.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Create
        public IActionResult Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name");
            return View();
        }

        // POST: Admin/ItemCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Status,ParentCategoryId")] ItemCategoryDto itemCategory)
        {
            if (ModelState.IsValid)
            {
                var userId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

                // Create domain entity
                var ic = new ItemCategory(
                    itemCategory.Name,
                    itemCategory.Description,
                    itemCategory.Status,
                    itemCategory.ParentCategoryId
                );


                // Audit info
                ic.UpdateAudit(userId);

                // First save to generate Id
                _context.ItemCategories.Add(ic);
                await _context.SaveChangesAsync();

                // Set BaseId = self Id if first revision
                ic.SetBaseId(ic.Id);
                _context.ItemCategories.Update(ic);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ParentCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name", itemCategory.ParentCategoryId);
            return View(itemCategory);
        }


        // GET: Admin/ItemCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategory = await _context.ItemCategories.FindAsync(id);
            if (itemCategory == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name", itemCategory.ParentCategoryId);
            return View(itemCategory);
        }

        // POST: Admin/ItemCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Description,IsCurrent,RevisionNumber,BaseId,Status,ParentCategoryId,Uid,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Id")] ItemCategory itemCategory)
        {
            if (id != itemCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCategoryExists(itemCategory.Id))
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
            ViewData["ParentCategoryId"] = new SelectList(_context.ItemCategories, "Id", "Name", itemCategory.ParentCategoryId);
            return View(itemCategory);
        }

        // GET: Admin/ItemCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategory = await _context.ItemCategories
                .Include(i => i.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCategory == null)
            {
                return NotFound();
            }

            return View(itemCategory);
        }

        // POST: Admin/ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var itemCategory = await _context.ItemCategories.FindAsync(id);
            if (itemCategory != null)
            {
                _context.ItemCategories.Remove(itemCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCategoryExists(long id)
        {
            return _context.ItemCategories.Any(e => e.Id == id);
        }
    }
}
