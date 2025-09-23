using RazorComponentRenderer;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.DisplayDate;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/displayDate", async (RazorComponentRendererService razorComponentRendererService, IDatastarService datastarService) =>
        {
            var today = DateTime.Now;

            var fragment = await razorComponentRendererService.RenderComponent(DisplayDate.CreateComponentParameters(today));

            await datastarService.PatchElementsAsync(fragment);
        });
    }
}