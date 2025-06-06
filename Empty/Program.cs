using System.Text.Json;
using System.Text.Json.Serialization;
using Empty;
using Microsoft.AspNetCore.Mvc;
using StarFederation.Datastar.DependencyInjection;
using DisplayDate = Empty.Components.DisplayDate;
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

app.MapGet("/removeDate", async (IDatastarServerSentEventService sse) => { await sse.RemoveFragmentsAsync("#date"); });

app.MapPost("/changeOutput", async (IDatastarServerSentEventService sse, IDatastarSignalsReaderService dsSignals) =>
{
    var signals = await dsSignals.ReadSignalsAsync<Signals>();
    var newSignals = signals with
    {
        Output = $"Your Input: {signals.Input}"
    };
    await sse.MergeSignalsAsync(newSignals.Serialize());
});

app.Run();

public record Signals(
    [property: JsonPropertyName("input")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string Input,
    [property: JsonPropertyName("output")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string Output)
{
    public string Serialize() =>
        JsonSerializer.Serialize(new Dictionary<string, object?>
        {
            ["output"] = Output,
            ["input"] = Input
        });
}