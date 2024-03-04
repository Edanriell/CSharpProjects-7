using BucksCoffeeShop.Infrastructure.HtmlCompress;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(
    new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // Cached for 24 hours.
            var response = ctx.Context.Response;
            var duration = 60 * 60 * 24; // 24h duration.
            response.Headers[HeaderNames.CacheControl] = "public,max-age=" + duration;
        }
    }
);

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseHtmlShrink();

app.Run();
