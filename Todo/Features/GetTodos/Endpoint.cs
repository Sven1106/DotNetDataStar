using Shared;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.GetTodos;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/getTodos", async (BlazorRenderer blazorRenderer, IDatastarService datastarService) =>
        {
            var fragment = await blazorRenderer.RenderComponent(new ListTodos());
            await datastarService.PatchElementsAsync(fragment);
        });
    }
}