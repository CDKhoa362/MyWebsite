using MyWebsite.Data;
using MyWebsite.Areas.Admin.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;

namespace MyWebsite.Areas.Admin.Controllers.Location
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
 
    public class DistrictController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DistrictController(ApplicationDbContext db)
        {
            _db = db;
        }


        // Create State SelectListItem
        private async Task<List<SelectListItem>> _getStateSelectList()
        {
            var stateSelectionList = await _db.States.Select(s => new SelectListItem
            {
                Text = s.StateName,
                Value = s.StateId.ToString()
            }).ToListAsync();
            return stateSelectionList;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 50)
        {
            // Paginition
            var totalRecords = await _db.Districts.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            // Search
            IQueryable<District> query = _db.Districts.AsNoTracking().Include(d => d.State);
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(d => EF.Functions.Collate(d.DistrictName, "SQL_Latin1_General_CP1_CI_AI").Contains(searchString) ||
                                         EF.Functions.Collate(d.State != null ? d.State.StateName : "", "SQL_Latin1_General_CP1_CI_AI").Contains(searchString)||
                                         d.DistrictId.Contains(searchString));
            }

            // View Index
            List<District> objDistrictsList = await query
                                                    .OrderBy(d => d.DistrictId)
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize).ToListAsync();

            return View(objDistrictsList);
        }

        /* Create Action */
        public async Task<IActionResult> Create()
        {
            District? district = new District
            {
                StatesSelectList = await _getStateSelectList()
            };
            return View(district);
        }

        [HttpPost]
        public async Task<IActionResult> Create(District? obj)
        {
            if (obj == null) return BadRequest("Dữ liệu không hợp lệ.");

            // Reload StatesSelectList if pageload error
            if (obj.StatesSelectList == null) obj.StatesSelectList = await _getStateSelectList();

            if (!string.IsNullOrEmpty(obj.DistrictId) && await _db.Wards.AnyAsync(w => w.DistrictId == obj.DistrictId))
            {
                ModelState.AddModelError("DistrictId", $"{obj.DistrictId} đã tồn tại");
            }

            if (ModelState.IsValid)
            {
                await _db.Districts.AddAsync(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "District");
            }
            return View(obj);
        }

        // Edit Action
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return BadRequest("ID không hợp lệ");

            District? district = await _db.Districts.FindAsync(id);
            if (district == null) return NotFound();
            else district.StatesSelectList = await _getStateSelectList();
            return View(district);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(District obj)
        {
            if (obj == null) return NotFound();
            if (ModelState.IsValid)
            {
                _db.Districts.Update(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            obj.StatesSelectList = await _getStateSelectList();
            return View(obj);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            District? district = await _db.Districts.FindAsync(id);
            if (district == null) return NotFound();
            return View(district);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(District obj)
        {
            if (ModelState.IsValid)
            {
                _db.Districts.Remove(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
