using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models.MyInfor;
using MyWebsite.Repositories;
namespace MyWebsite.Areas.Admin.Controllers
{
    public class UserPostingController : Controller
    {
        private readonly IRepository<UserPosting> _repository; 
        private readonly UserManager<IdentityUser> _userManager;

        // UserPosting Constructor 
        public UserPostingController(IRepository<UserPosting> repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }


        //public async IActionResult Index()
        //{
        //   var userJobPostings = await _repository.GetAllAsync(); 
        //    return View(userJobPostings);
        //}
    }
}
