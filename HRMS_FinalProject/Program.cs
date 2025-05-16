using HRMS_FinalProject.Data;
using HRMS_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRMS_FinalProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("constr"));
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await CreateRolesAndAdminUser(services);
                await AddDefaultCompany(services);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Employees}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }

        private static async Task CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Employee" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@company.com");
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = "admin@company.com",
                    Email = "admin@company.com"
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        private static async Task AddDefaultCompany(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            if (!await context.Companies.AnyAsync())
            {
                var companyInstance = new Company();

                companyInstance.Name = "Tech Solutions";
                companyInstance.Address = "123 Tech Lane, NY";
                companyInstance.Phone = "(555) 123-4567";
                companyInstance.Email = "info@techsolutions.com";
                companyInstance.EstablishedDate = new DateTime(2012, 01, 01);
                context.Companies.Add(companyInstance);
                await context.SaveChangesAsync();
            }
        }
    }
}