using System.Text.Json.Serialization;
using Empty;
using Empty.Components;
using Empty.Components.Partials;
using Microsoft.AspNetCore.Components.Web;
using StarFederation.Datastar.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents();
builder.Services.AddScoped<HtmlRenderer>();
builder.Services.AddScoped<BlazorRenderer>();
builder.Services.AddDatastar();
builder.Services.AddAntiforgery();
var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/not-found");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.MapGet("/displayDate", async (BlazorRenderer blazorRenderer, IDatastarService datastarService) =>
{
    var today = DateTime.Now;
    var displayDate = new DisplayDate
    {
        Today = today
    };
    var fragment = await blazorRenderer.RenderComponent(displayDate);
    await datastarService.PatchElementsAsync(fragment);
});

app.MapPost("/removeDate",
    async (IDatastarService datastarService) => { await datastarService.RemoveElementAsync("#date"); });

app.MapPost("/changeOutput", async (IDatastarService datastarService) =>
{
    var signals = await datastarService.ReadSignalsAsync<Signals>();
    if (signals is not null)
    {
        await datastarService.PatchSignalsAsync(signals with
        {
            Output = $"Your Input: {signals.Input}"
        });
    }
});

app.Run();

public record Signals(
    [property: JsonPropertyName("input")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string Input,
    [property: JsonPropertyName("output")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string Output
);

public class Test : Attribute
{
    public string Foo()
    {
        return "bar";
    }
}
// public partial class DisplayDate
// {
//     public record Props(DateTime Today)
//     {
//         public static implicit operator Dictionary<string, object>(Props props) => new()
//         {
//             [nameof(props.Today)] = props.Today
//         };
//     }
// }