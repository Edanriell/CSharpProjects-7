using EntityFrameworkPatterns.DataContext;
using EntityFrameworkPatterns.Repository_UOW;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ThemeParkDbContext>(
    (provider, options) =>
    {
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        options.UseLoggerFactory(loggerFactory);
        options.UseInMemoryDatabase("ThemeParkRides");
    },
    ServiceLifetime.Transient
);

builder.Services.AddTransient<IThemeParkDbContext, ThemeParkDbContext>();
builder.Services.AddTransient<IAttractionService, AttractionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

using (var context = new ThemeParkDbContext(new DbContextOptions<ThemeParkDbContext>()))
{
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

