using RentACar.Core.Services;
using RentACar.Web.Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "RentACar.Web"))
    .AddJsonFile("appsettings.json", true)
    .AddUserSecrets<Program>();
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.RegisterDbContext();
builder.AddIdentity();

builder.Services.RegisterRepositories();

builder.Services.RegisterUserDefinedServices();

builder.Services.AddHttpClient<LocationService>();
builder.Services.AddHttpClient();


builder.Services.RegisterAutoMapper();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();
await app.RunAsync();
