using BlazorRenderer;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.DisplayDate;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/displayDate", async (BlazorRendererService blazorRendererService, IDatastarService datastarService) =>
        {
            var today = DateTime.Now;

            var fragment = await blazorRendererService.RenderComponent(DisplayDate.CreateComponentParameters(today));

            await datastarService.PatchElementsAsync(fragment);
        });
    }
}