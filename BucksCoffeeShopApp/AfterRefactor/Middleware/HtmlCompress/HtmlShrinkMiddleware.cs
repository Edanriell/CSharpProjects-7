namespace BucksCoffeeShop.Infrastructure.HtmlCompress;

public class HtmlShrinkMiddleware
{
    private readonly RequestDelegate _next;

    public HtmlShrinkMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        using var buffer = new MemoryStream();

        var stream = context.Response.Body;
        context.Response.Body = buffer;

        await _next(context);

        buffer.Seek(0, SeekOrigin.Begin);

        var compressedHtmlStream = new HtmlShrinkStream(stream);

        buffer.Seek(0, SeekOrigin.Begin);

        await buffer.CopyToAsync(compressedHtmlStream);
        context.Response.Body = compressedHtmlStream;
    }
}

public static class HtmlShrinkMiddlewareExtensions
{
    public static IApplicationBuilder UseHtmlShrink(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HtmlShrinkMiddleware>();
    }
}
