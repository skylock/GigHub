using GigHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;

namespace GigHub.Data
{
    public class DatabaseSeeder
    {
        private static ApplicationDbContext _context;
        private static UserManager<ApplicationUser> _userManager;
        private static RoleManager<ApplicationRole> _roleManager;

        public static async Task Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;


            _context.Database.EnsureCreated();

            await SeedUserAndRoles();

            //await SeedGenres();
        }

        private static async Task SeedGenres()
        {
            if (!_context.Genres.Any())
            {
                var genres = new Genre[]
                {
                    new Genre {Id = 1, Name = "Jazz"},
                    new Genre {Id = 2, Name = "Blues"},
                    new Genre {Id = 3, Name = "Rock"},
                    new Genre {Id = 4, Name = "Country"}
                };

                _context.Genres.AddRange(genres);
                await _context.SaveChangesAsync();
            }
        }

        private static async Task SeedUserAndRoles()
        {
            var adminId1 = "";
            var adminId2 = "";

            var role1 = "Admin";
            var desc1 = "Administrator role.";

            var role2 = "Member";
            var desc2 = "Member role.";

            var password = "Admin12345x*";

            if (await HasRole(role1))
            {
                await _roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }

            if (await HasRole(role2))
            {
                await _roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            if (await HasUser("jim@gighub.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "jim@gighub.com",
                    Email = "jim@gighub.com",
                    FirstName = "Jim",
                    LastName = "Doe",
                    PhoneNumber = "508-676-1046"
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, password);
                    await _userManager.AddToRoleAsync(user, role1);
                }

                adminId1 = user.Id;
            }

            if (await HasUser("tim@gighub.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "tim@gighub.com",
                    Email = "tim@gighub.com",
                    FirstName = "Tim",
                    LastName = "Doe",
                    PhoneNumber = "555-676-1046"
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, password);
                    await _userManager.AddToRoleAsync(user, role2);
                }

                adminId2 = user.Id;
            }
        }

        private static async Task<bool> HasUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }

        private static async Task<bool> HasRole(string role1)
        {
            return await _roleManager.FindByNameAsync(role1) == null;
        }
    }
}
