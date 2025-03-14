using MyWebsite.Data;
using MyWebsite.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyWebsite.Controllers.Location
{
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CountryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Country> query = _db.Countries;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.CountryName.Contains(searchString));
            }

            List<Country> countriesList = await query.ToListAsync();
            return View(countriesList);

        }

        /*--CREATE ACTION--*/
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Country obj)
        {
            var existCountry = await _db.Countries.FirstOrDefaultAsync(c => c.CountryId == obj.CountryId);
            if (existCountry != null) { ModelState.AddModelError("Country", $"{obj.CountryId} đã tồn tại");};

            if (ModelState.IsValid)
            {
                _db.Countries.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Country");
            }
            return View(obj);
        }


        /*--EDIT ACTION--*/



        [HttpGet] // Mặc định 1 action là http get
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();
            Country? country = await _db.Countries.FindAsync(id);
            if (country == null) return NotFound();
            return View(country);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Country obj)
        {
            if (ModelState.IsValid)
            {
                _db.Countries.Update(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /*--DELETE ACTION--*/
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();
            Country? country = await _db.Countries.FindAsync(id);
            if (country == null) return NotFound();
            return View(country);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Country obj)
        {
            _db.Countries.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
