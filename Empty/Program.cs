using Empty;
using Empty.Components;
using Microsoft.AspNetCore.Mvc;
using StarFederation.Datastar.DependencyInjection;
using Index = Empty.Components.Index;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBlazorRenderer();
builder.Services.AddDatastar();
var app = builder.Build();

app.MapGet("/", async ([FromServices] BlazorRenderer blazorRenderer) =>
{
    var result = await blazorRenderer.RenderComponent<Index>(
        new Index.Props(
            "Datastar Minimal Blazor example",
            "Hello there, this is the minimal api + blazor demo;")
    );
    return result;
});

app.MapGet("/displayDate", async ([FromServices] BlazorRenderer blazorRenderer, IDatastarServerSentEventService sse) =>
{
    var today = DateTime.Now;
    var fragment = await blazorRenderer.RenderComponentString<DisplayDate>(new DisplayDate.Props(today));
    await sse.MergeFragmentsAsync(fragment);
});

// removeDate - removing a fragment
app.MapGet("/removeDate", async (IDatastarServerSentEventService sse) => { await sse.RemoveFragmentsAsync("#date"); });
app.Run();