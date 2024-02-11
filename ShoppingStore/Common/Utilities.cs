using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShoppingStore.Common
{
    public static class Utilities
    {
        public static async Task DefineDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator") && !await roleManager.RoleExistsAsync("Customer"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "Administrator"
                };
                await roleManager.CreateAsync(role);
                role = new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "Customer"
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
