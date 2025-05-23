using RentACar.Core.Services;
using RentACar.Web.Infrastructure.Extensions;
using RentACar.Web.Infrastructure.ModelBinderProviders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true)
    .AddJsonFile("secrets.json", true)
    .AddUserSecrets<Program>();

// Add services to the container.

builder.RegisterDbContext();

builder.AddIdentity();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews(options =>
    {
        options.ModelBinderProviders.Insert(0, new InsuranceBenefitCustomModelBinderProvider());
    });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = "/Home/Error/401";
    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
});

builder.Services.RegisterRepositories();

builder.Services.RegisterUserDefinedServices();

builder.Services.RegisterRentalStatusUpdaterService();
builder.Services.AddHttpClient<LocationService>();

builder.Services.RegisterAutoMapper();

WebApplication app = builder.Build();

app.ApplyMigrations();
app.SeedDatabase();
await app.SeedAdminAndRoles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "Errors",
    pattern: "{controller=Home}/{action=Index}/{statusCode?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
