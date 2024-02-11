using Microsoft.AspNetCore.Identity;

namespace ShoppingStore.Common.Seed
{
    public class DbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            await Utilities.DefineDefaultRolesAsync(_roleManager);
            string adminRole = "Administrator";
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin123!";

            // Create admin role if it doesn't exist
            if (await _roleManager.FindByNameAsync(adminRole) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Create admin user if it doesn't exist
            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    // Assign admin role to the admin user
                    await _userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}
