using Microsoft.AspNetCore.Components.Web;

namespace Empty;

public static class BlazorRendererExtensions
{
    public static void AddBlazorRenderer(this IServiceCollection services)
    {
        //Check if ILoggerFactory is registered
        if (services.All(svc => svc.ServiceType != typeof(ILoggerFactory)))
        {
            throw new InvalidOperationException("ILoggerFactory must be registered before BlazorRenderer.");
        }
        
        services.AddSingleton<HtmlRenderer>();
        services.AddSingleton<BlazorRenderer>();
    }
}