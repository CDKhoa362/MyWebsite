using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyWebsite.Models.MyInfor;
using MyWebsite.Repositories;
using MyWebsite.Areas.Admin.Models.Location;
using MyWebsite.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserPostingViewModel userPostingVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var userPosting = new UserPosting
                {
                    UserId = _userManager.GetUserId(User),
                    FirstName = userPostingVm.FirstName,
                    LastName = userPostingVm.LastName,
                    Major = userPostingVm.Major,
                    Gender = userPostingVm.Gender,
                    DOB = userPostingVm.DOB,
                    HouseNumber = userPostingVm.HouseNumber,
                    Address = userPostingVm.Address,
                };

                if (userPostingVm.Avatar != null && userPostingVm.Avatar.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await userPostingVm.Avatar.CopyToAsync(memoryStream);
                        userPosting.Avatar = memoryStream.ToArray();
                    }
                }


                await _repository.AddAsync(userPosting);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
