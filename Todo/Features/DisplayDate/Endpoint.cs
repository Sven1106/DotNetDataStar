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

            var fragment = await blazorRenderer.RenderComponent(DisplayDate.CreateComponentParameters(today));

            await datastarService.PatchElementsAsync(fragment);
        });
    }
}