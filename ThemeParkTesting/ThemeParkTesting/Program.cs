using System.Reflection;
using ThemePark.DataContext;
using ThemePark.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<ThemeParkDbContext>(connectionString);

builder.Services.AddTransient<IThemeParkDbContext, ThemeParkDbContext>();
builder.Services.AddTransient<IAttractionService, AttractionService>();
builder.Services.AddTransient<ILocationService, LocationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
