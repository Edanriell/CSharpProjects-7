using ThemePark.Data.DataContext;
using ThemePark.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<ThemeParkDbContext>(
    connectionString,
    options => options.MigrationsAssembly("ThemePark.Data")
);

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
