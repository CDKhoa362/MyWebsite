using MyWebsite.Data;
using MyWebsite.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MyWebsite.Controllers.Location
{
    public class WardController : Controller
    {
        private readonly ApplicationDbContext _db;
        public WardController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        // Create District SelectListItem
        private async Task<List<SelectListItem>> _getDistrictSelectList()
        {
                var districtSelectionList = await _db.Districts.Select(d => new SelectListItem { Text = d.DistrictName, Value = d.DistrictId.ToString() }).ToListAsync();
            return districtSelectionList;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 50)
        {
            var totalRecords = await _db.Wards.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            IQueryable<Ward> query = _db.Wards.Include(w => w.District);
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(w => w.WardId.Contains(searchString) ||
                w.WardName.Contains(searchString)||
                w.District != null && w.District.DistrictName.Contains(searchString));
            }
            List<Ward> objWardList = await query
                                           .OrderBy(w => w.WardId)
                                           .Skip((page - 1) * pageSize)
                                           .Take(pageSize).ToListAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(objWardList);
        }

        // Create Action

        public async Task <IActionResult> Create()
        {
            Ward ward = new Ward
            {
                DistrictSelectedList = await _getDistrictSelectList()
            };
            return View(ward);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ward obj)
        {
            if (await _db.Wards.AnyAsync(w => w.WardId == obj.WardId))
            {
                ModelState.AddModelError("WardId", $"{obj.WardId} đã tồn tại");
            }
            if (ModelState.IsValid)
            {
                _db.Wards.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if(obj.DistrictSelectedList == null)
            {
                obj.DistrictSelectedList = await _getDistrictSelectList();
            }
            return View(obj);
        }

        // Edit Action
        public async Task<IActionResult> Edit(string? id)
        {
            if(id == null) return NotFound();
            Ward? ward = await _db.Wards.FindAsync(id);
            if (ward == null) return NotFound();
            else { 
                ward.DistrictSelectedList = await _getDistrictSelectList();
            }
            return View(ward);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Ward? obj)
        {
            if (obj == null) return NotFound();
            if (ModelState.IsValid) 
            {
                _db.Wards.Update(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Delete Action
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            Ward? ward = await _db.Wards.FindAsync(id);
            if (ward == null) return NotFound();
            return View(ward);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Ward obj){
            if (ModelState.IsValid)
            {
                _db.Wards.Remove(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
