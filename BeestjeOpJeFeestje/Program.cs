using BeestjeOpJeFeestje.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddIdentity<Account, IdentityRole>()
            .AddEntityFrameworkStores<BeestjeOpJeFeestjeContext>()
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
        });

        builder.Services.AddDbContext<BeestjeOpJeFeestjeContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BeestjeOpJeFeestje"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        IServiceProvider serviceProvider = app.Services.CreateScope().ServiceProvider;
        await SeedDefaultRoles(serviceProvider);
        await SeedDefaultUser(serviceProvider);

        app.Run();
    }

    private static async Task SeedDefaultUser(IServiceProvider serviceProvider)
    {
        UserManager<Account> userManager = serviceProvider.GetRequiredService<UserManager<Account>>();

        // If there are already users in the database, don't add the default user.
        if (userManager.Users.Any())
        {
            return;
        }

        Account user = new Account
        {
            UserName = "admin",
            Name = "admin",
            Email = "admin@beestjeopjefeestje.nl",
            Address = "admin",
            PhoneNumber = "1234567"
        };

        await userManager.CreateAsync(user, "Admin@123");
        await userManager.AddToRoleAsync(user, "Admin");
    }

    private static async Task SeedDefaultRoles(IServiceProvider serviceProvider)
    {
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // If there are already roles in the database, don't add the default roles.
        if (roleManager.Roles.Any())
        {
            return;
        }

        await roleManager.CreateAsync(new IdentityRole("Admin"));
        await roleManager.CreateAsync(new IdentityRole("Customer"));
    }
}
