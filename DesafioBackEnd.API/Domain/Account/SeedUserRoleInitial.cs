using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Domain.Account.Interface;
using DesafioBackEnd.API.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace DesafioBackEnd.API.Domain.Account
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                ApplicationRole role = new()
                {
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                ApplicationRole role = new()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("usuario@gmail.com").Result == null)
            {
                ApplicationUser user = new()
                {
                    UserName = "usuario@gmail.com",
                    Email = "usuario@gmail.com",
                    NormalizedUserName = "usuario@gmail.com".ToUpper(),
                    NormalizedEmail = "usuario@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };                                      

                IdentityResult result = _userManager.CreateAsync(user, "Saysay@2025!@").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, UserRole.User.ToString()).Wait();
                }
            }

            if (_userManager.FindByEmailAsync("admin@admin").Result == null)
            {
                ApplicationUser user = new()
                {
                    UserName = "admin@admin",
                    Email = "admin@admin",
                    NormalizedUserName = "admin@admin".ToUpper(),
                    NormalizedEmail = "admin@admin".ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult result = _userManager.CreateAsync(user, "Saysay@2025!@").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, UserRole.Admin.ToString()).Wait();
                }
            }
        }
    }
}
