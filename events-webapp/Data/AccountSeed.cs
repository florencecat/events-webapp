using wa_dev_coursework.Models.EventsContext;
using Microsoft.AspNetCore.Identity;

namespace wa_dev_coursework.Data
{
    public class AccountsSeed
    {
        public static async Task SeedUserAndRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Создание ролей администратора и пользователя
            if (await roleManager.FindByNameAsync("admin") == null)
                await roleManager.CreateAsync(new IdentityRole("admin"));

            if (await roleManager.FindByNameAsync("user") == null)
                await roleManager.CreateAsync(new IdentityRole("user"));

            // Создание Администратора
            string adminName = "admin";
            string adminPassword = "1qaz!QAZ";
            if (await userManager.FindByNameAsync(adminName) == null)
            {
                User admin = new User { UserName = adminName };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded) await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}