using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorRenderer.DependencyInjection.ServiceCollectionExtensionMethods;

public static class BlazorRendererExtensions
{
    public static IServiceCollection AddBlazorRenderer(this IServiceCollection services)
    {
        services.AddScoped<HtmlRenderer>();
        services.AddScoped<BlazorRendererService>();
        return services;
    }
}