using MyWebsite.Data;
using MyWebsite.Areas.Admin.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;

namespace MyWebsite.Areas.Admin.Controllers.Location
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StateController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Index Action
        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 50)
        {
            var totalRecords = await _db.States.CountAsync();
            var totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);

            IQueryable<State> query = _db.States.Include(s => s.Country);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => EF.Functions.Collate(s.StateName, "SQL_Latin1_General_CP1_CI_AI").Contains(searchString) ||
                                         EF.Functions.Collate(s.Country != null ? s.Country.CountryName : "", "SQL_Latin1_General_CP1_CI_AI").Contains(searchString) ||
                                         s.StateId.Contains(searchString));

            }
            List<State> objStatesList = await query
                                                .OrderBy(s => s.StateId)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPage;
            return View(objStatesList);
        }



        private async Task<List<SelectListItem>> GetCountriesSelectList()
        {
            var countrySelectList = await _db.Countries.Select(c => new SelectListItem { Text = c.CountryName, Value = c.CountryId.ToString() }).ToListAsync();
            return countrySelectList;
        }

        /* Create Action */
        public async Task<ActionResult> Create()
        {
            State? state = new State
            {
                CountriesSelectedList = await GetCountriesSelectList()
            };
            return View(state);
        }


        [HttpPost]
        public async Task<ActionResult> Create(State obj)
        {
            var existingState = await _db.States.FirstOrDefaultAsync(s => s.StateId == obj.StateId);
            if (existingState != null) { ModelState.AddModelError("StateId", $"{obj.StateId} đã tồn tại"); };

            if (ModelState.IsValid)
            {
                _db.States.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            obj.CountriesSelectedList = await GetCountriesSelectList();
            return View(obj);
        }


        /*  Action */
        public async Task<IActionResult> Edit(string? id)
        {

            if (id == null) return NotFound();
            State? state = await _db.States.FindAsync(id);
            if (state == null) return NotFound();
            else
            {
                state.CountriesSelectedList = await GetCountriesSelectList();
            }

            return View(state);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(State? obj)
        {
            if (obj == null) return NotFound();
            if (ModelState.IsValid)
            {
                _db.States.Update(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        /*Delete Action*/
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            State? state = await _db.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(State obj)
        {
            _db.States.Remove(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
