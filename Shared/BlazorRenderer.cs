using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Shared;

public class BlazorRenderer(HtmlRenderer htmlRenderer)
{
    // ReSharper disable once UnusedTypeParameter
    public readonly record struct ComponentParameters<TComponent>(IReadOnlyDictionary<string, object?> Parameters) where TComponent : IComponent;

    public Task<string> RenderComponent<T>(ComponentParameters<T> parameters) where T : IComponent =>
        RenderComponent<T>(ParameterView.FromDictionary(
            parameters.Parameters as Dictionary<string, object?> ?? new Dictionary<string, object?>(parameters.Parameters)
        ));

    private Task<string> RenderComponent<T>(ParameterView parameters) where T : IComponent
    {
        return htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await htmlRenderer.RenderComponentAsync<T>(parameters);
            return output.ToHtmlString();
        });
    }
}

public static class BlazorRendererExtensions
{
    public static IServiceCollection AddBlazorRenderer(this IServiceCollection services)
    {
        services.AddScoped<HtmlRenderer>();
        services.AddScoped<BlazorRenderer>();
        return services;
    }
}