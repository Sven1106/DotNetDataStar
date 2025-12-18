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
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseLiveReload();
}

app.MapRazorComponents<App>();
app.MapAllEndpoints();
app.UseAntiforgery();
app.Run();