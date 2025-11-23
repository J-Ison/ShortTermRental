using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortTermRental.Components;
using ShortTermRental.Data;

var builder = WebApplication.CreateBuilder(args);

// ---- DATABASE + IDENTITY ----
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ---- RAZOR COMPONENTS (.NET 8 STYLE) ----
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages(); // Needed for Identity UI pages

var app = builder.Build();

// ---- MIDDLEWARE ----
// Configure the middleware pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Use custom error page in production.
    app.UseHsts(); // Enable HSTS for production.
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); // Enable routing middleware.

app.UseAuthentication(); // Enable authentication middleware.
app.UseAuthorization(); // Enable authorization middleware.

// 🔥 Add this line:
app.UseAntiforgery(); // Enable antiforgery middleware


// ---- MAP APP ROOT (Razor Components) ----
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// ---- MAP RAZOR PAGES (Identity, etc.) ----
app.MapRazorPages();

app.Run();
