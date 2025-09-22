using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Shared;

public class BlazorRenderer(HtmlRenderer htmlRenderer)
{
    public Task<string> RenderComponent<T>(T component) where T : IComponent
    {
        var parameters = component
            .GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanRead)
            .ToDictionary(
                p => p.Name,
                p => p.GetValue(component),
                StringComparer.OrdinalIgnoreCase
            );

        return htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await htmlRenderer.RenderComponentAsync<T>(ParameterView.FromDictionary(parameters));
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