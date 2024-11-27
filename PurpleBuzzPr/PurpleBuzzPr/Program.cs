using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
        options.Lockout.MaxFailedAccessAttempts = 10;
    }
)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDBContext>();

builder.Services.AddDbContext<AppDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")
));
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
