using Microsoft.AspNetCore.Identity;
using MyWebsite.Models.Location;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyWebsite.Models.MyInfor
{
    public class UserPosting
    {
        public string UserPostingId { get; set; } = null!;
        public string? AvatarPath { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DOB { get; set; }
        public bool Gender { get; set; } = true;

        // ADDRESS
        public string HouseNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

        // USER
        public string UserId { get; set; } = null!;
        public IdentityUser? User { get; set; }



    }
}   
