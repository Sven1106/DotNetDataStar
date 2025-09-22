using Shared;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.DisplayDate;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/displayDate", async (BlazorRenderer blazorRenderer, IDatastarService datastarService) =>
        {
            var today = DateTime.Now;
            var displayDate = new DisplayDate
            {
                Today = today
            };
            var fragment = await blazorRenderer.RenderComponent(displayDate);

            var pv1 = RenderParams.For(RenderParams.P<DisplayDate, DateTime>(c => c.Today, today));

            await datastarService.PatchElementsAsync(fragment);
        });
    }
}