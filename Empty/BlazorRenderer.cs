using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;

namespace Empty;

public class BlazorRenderer(HtmlRenderer htmlRenderer)
{
    public Task<string> RenderComponent<T>() where T : IComponent
        => RenderComponent<T>(ParameterView.Empty);

    public Task<string> RenderComponent<T>(Dictionary<string, object?> parameters) where T : IComponent
        => RenderComponent<T>(ParameterView.FromDictionary(parameters));

    private Task<string> RenderComponent<T>(ParameterView parameters) where T : IComponent
    {
        return htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await htmlRenderer.RenderComponentAsync<T>(parameters);
            return output.ToHtmlString();
        });
    }
}