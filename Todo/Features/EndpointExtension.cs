
namespace Todo.Features;

public static class EndpointExtension
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder endpoints)
    {
        DisplayDate.Endpoint.Map(endpoints);
        GetTodos.Endpoint.Map(endpoints);
        return endpoints;
    }
}