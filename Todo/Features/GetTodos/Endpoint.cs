using RazorComponentRenderer;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.GetTodos;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/getTodos", async (RazorComponentRendererService razorComponentRendererService, IDatastarService datastarService) =>
        {
            var fragment = await razorComponentRendererService.RenderComponent(
                ListTodos.CreateComponentParameters([("test1", false), ("test2", true)])
            );
            await datastarService.PatchElementsAsync(fragment);
        });
    }
}