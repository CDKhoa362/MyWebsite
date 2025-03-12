using Microsoft.AspNetCore.Identity;
using Diary_App.Constants;

namespace Diary_App.Data
{
    public class UserSeeder
    {
        public static async Task SeederUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await CreateUserWithRole(userManager, "cdkhoa3622@gmail.com", "Admin@123", Roles.ADMIN);
            await CreateUserWithRole(userManager, "noir06032002@gmail.com", "Admin@123", Roles.USER);


        }

        private static async Task CreateUserWithRole(UserManager<IdentityUser> userManager,
                                                    string email,
                                                    string password,
                                                    string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email

                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email: {user.Email}! \n Error: {string.Join(", ", result.Errors)}");
                }
            }
        }

    }
}
