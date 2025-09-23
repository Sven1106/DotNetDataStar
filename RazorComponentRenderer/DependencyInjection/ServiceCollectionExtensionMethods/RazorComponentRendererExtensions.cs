using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace RazorComponentRenderer.DependencyInjection.ServiceCollectionExtensionMethods;

public static class RazorComponentRendererExtensions
{
    public static IServiceCollection AddRazorComponentRenderer(this IServiceCollection services)
    {
        services.AddScoped<HtmlRenderer>();
        services.AddScoped<RazorComponentRendererService>();
        return services;
    }
}