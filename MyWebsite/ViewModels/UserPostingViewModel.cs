﻿using System.ComponentModel.DataAnnotations; 
namespace MyWebsite.ViewModels
{
    public class UserPostingViewModel
    {   
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Major { get; set; } = null;  
        public DateOnly? DOB { get; set; } = null;
        public bool Gender { get; set; } = true;

        // ADDRESS
        public string? HouseNumber { get; set; } = null;
        public string? Address { get; set; } = null;

        // AVATAR
        public IFormFile? Avatar { get; set; }
    }
}
