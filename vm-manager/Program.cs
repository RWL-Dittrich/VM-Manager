using System.Reflection;
using Microsoft.EntityFrameworkCore;
using vm_manager;
using vm_manager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                       builder.Configuration.GetConnectionString("DatabaseConnection");

//Add Database
builder.Services.AddDbContext<ApplicationDbContext>(builder =>
    builder.UseNpgsql(connectionString,
        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly).EnableRetryOnFailure()));


builder.Services.AddScoped<CommandService>();
builder.Services.AddSingleton<JobService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    ApplyMigrations(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


void ApplyMigrations(ApplicationDbContext context)
{
    if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();
}