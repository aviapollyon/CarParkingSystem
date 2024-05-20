using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarParkingSystem.Areas.Identity.Data;
using CarParkingSystem.EmailSender;
using Microsoft.AspNetCore.Http;
using CarParkingSystem;
using CarParkingSystem.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMailSender, EmailSender>();
builder.Services.AddScoped<ParkingBayService>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Login/Login";
    options.LogoutPath = $"/Login/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = service.GetRequiredService<ApplicationDbContext>();

        // Create database on the first time run on the database apply only migration first then database create auto.
        if (context.Database.CanConnect())
        {
            Console.WriteLine("The database already exists.");
        }
        else
        {
            context.Database.Migrate();
        }

        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

        // Add default roles
        await ContextSeed.seedRolesAsync(roleManager);

        // Add default Admin who manages the web application
        await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
