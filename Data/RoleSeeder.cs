using Diary_App.Constants;
using Microsoft.AspNetCore.Identity;

namespace Diary_App.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoleAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.ADMIN))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN));
            }

            if(! await roleManager.RoleExistsAsync(Roles.USER))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.USER));
            }
        }
    }
}
