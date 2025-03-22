using Microsoft.AspNetCore.Identity;
using MyWebsite.Areas.Admin.Models.Location;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyWebsite.Models.MyInfor
{
    public class UserPosting
    {
        public string UserPostingId { get; set; } = Guid.NewGuid().ToString();
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public DateOnly? DOB { get; set; } = null;
        public bool Gender { get; set; } = true;
        public string? Major { get; set; } = null;

        // ADDRESS
        public string? HouseNumber { get; set; } = null;    
        public string? Address { get; set; } = null;

        // AVATAR
        public byte[]? Avatar { get; set; }

        // USER
        public string UserId { get; set; } = null!;
        public IdentityUser? User { get; set; }
    }
}   
