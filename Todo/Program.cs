using RazorComponentRenderer;
using RazorComponentRenderer.DependencyInjection.ServiceCollectionExtensionMethods;
using StarFederation.Datastar.DependencyInjection;
using Todo.Components;
using Todo.Features;
using Westwind.AspNetCore.LiveReload;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAntiforgery();
builder.Services.AddRazorComponents();

builder.Services.AddLiveReload();
builder.Services.AddDatastar();

builder.Services.AddRazorComponentRenderer();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseLiveReload();
}

app.UseAntiforgery();
app.MapRazorComponents<App>();
app.MapAllEndpoints();
app.Run();