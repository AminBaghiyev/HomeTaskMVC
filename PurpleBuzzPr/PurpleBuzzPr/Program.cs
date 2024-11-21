var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.MapControllerRoute(
    name: "singlework",
    pattern: "{controller=SingleWork}/{action=Index}/{id?}"
);

app.Run();
