using GigHub.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GigHub.Data
{
    public class DatabaseSeeder
    {
        public static async Task Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            string adminId1 = "";
            string adminId2 = "";

            string role1 = "Admin";
            string desc1 = "Administrator role.";

            string role2 = "Member";
            string desc2 = "Member role.";

            string password = "Admin12345x*";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }

            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            if (await userManager.FindByNameAsync("jim@gighub.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "jim@gighub.com",
                    Email = "jim@gighub.com",
                    FirstName = "Jim",
                    LastName = "Doe",
                    PhoneNumber = "508-676-1046"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }

                adminId1 = user.Id;
            }

            if (await userManager.FindByNameAsync("tim@gighub.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "tim@gighub.com",
                    Email = "tim@gighub.com",
                    FirstName = "Tim",
                    LastName = "Doe",
                    PhoneNumber = "555-676-1046"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }

                adminId2 = user.Id;
            }
        }
    }
}
