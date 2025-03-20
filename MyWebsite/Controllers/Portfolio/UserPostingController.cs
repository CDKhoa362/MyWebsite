using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyWebsite.Models.MyInfor;
using MyWebsite.Repositories;
using MyWebsite.Areas.Admin.Models.Location;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyWebsite.Controllers.Portfolio
{
    public class UserPostingController : Controller
    {
        // Inject Repository, UserManager
        private readonly IRepository<UserPosting> _repository;
        private readonly UserManager<IdentityUser> _userManager;  
        
      

        // Constructor
        public UserPostingController(IRepository<UserPosting> repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }


        // Index
        public async Task<IActionResult> Index()
        {
            var userPosting = await _repository.GetAllAsync();    
            return View(userPosting);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        //public async Task<IActionResult> Create(UserPosting userPosting)
        //{
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
