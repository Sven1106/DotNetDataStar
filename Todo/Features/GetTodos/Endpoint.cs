using BlazorRenderer;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.GetTodos;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/getTodos", async (BlazorRendererService blazorRendererService, IDatastarService datastarService) =>
        {
            var fragment = await blazorRendererService.RenderComponent(ListTodos.CreateComponentParameters([("test1", false), ("test2", true)]));
            await datastarService.PatchElementsAsync(fragment);
        });
    }
}