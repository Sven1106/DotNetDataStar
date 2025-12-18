using Microsoft.AspNetCore.ResponseCompression;
using Westwind.AspNetCore.LiveReload;
using StarFederation.Datastar.DependencyInjection;
using RazorComponentRenderer.DependencyInjection.ServiceCollectionExtensionMethods;
using Todo;
using Todo.Components;
using Todo.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddDatastar();

builder.Services.AddLiveReload();

builder.Services.AddSingleton<InMemoryDatabase>();
builder.Services.AddSingleton<NotificationService>();

builder.Services.AddRazorComponentRenderer();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes =
    [
        "text/event-stream",
    ];
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = System.IO.Compression.CompressionLevel.Fastest; });
builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = System.IO.Compression.CompressionLevel.SmallestSize; });
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseLiveReload();
}

app.UseResponseCompression();
app.MapRazorComponents<App>();
app.MapAllEndpoints();
app.UseAntiforgery();
app.Run();