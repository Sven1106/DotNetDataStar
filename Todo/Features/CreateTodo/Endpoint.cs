using Microsoft.AspNetCore.Mvc;
using RazorComponentRenderer;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.CreateTodo;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/createTodo",
            async ([FromForm] CreateTodoRequest request, IDatastarService datastarService, InMemoryDatabase database, RazorComponentRendererService razorComponentRendererService,
                NotificationService notificationService, CancellationToken cancellationToken) =>
            {
                var item = new TodoItem(Guid.NewGuid(), request.Name, false);
                database.AddItemAsync(item);
                notificationService.NotifySubscribers();

                var fragment = await razorComponentRendererService.RenderComponent(
                    CreateTodoInput.CreateComponentParameters()
                );
                 await datastarService.PatchElementsAsync(fragment, cancellationToken);
            });
    }
}

public record CreateTodoRequest(string Name);