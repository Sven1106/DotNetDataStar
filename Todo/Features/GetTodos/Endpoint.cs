using RazorComponentRenderer;
using StarFederation.Datastar.DependencyInjection;

namespace Todo.Features.GetTodos;

public static class Endpoint
{
    public static void Map(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/getTodos",
            async (HttpContext httpContext,
                RazorComponentRendererService razorComponentRendererService,
                IDatastarService datastarService,
                InMemoryDatabase database,
                NotificationService notificationService,
                CancellationToken cancellationToken) =>
            {
                var subscriberId = httpContext.Connection.Id;

                try
                {
                    await notificationService.AddSubscriberWithCallBack(subscriberId, async () =>
                    {
                        var items = database.ItemsById.Values.ToList();

                        var fragment = await razorComponentRendererService.RenderComponent(
                            ListTodos.CreateComponentParameters(items.Select(i => (i.Id, Title: i.Name, i.IsComplete)).ToList())
                        );
                        await datastarService.PatchElementsAsync(fragment, cancellationToken);
                    });
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    notificationService.RemoveSubscriber(subscriberId);
                }
            });
    }
}