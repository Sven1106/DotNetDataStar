using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace RazorComponentRenderer;

public class RazorComponentRendererService(HtmlRenderer htmlRenderer)
{
    public Task<string> RenderComponent<T>(ComponentParameters<T> parameters) where T : IComponent =>
        RenderComponent<T>(ParameterView.FromDictionary(
            parameters.Parameters as Dictionary<string, object?> ?? new Dictionary<string, object?>(parameters.Parameters)
        ));

    private Task<string> RenderComponent<T>(ParameterView parameters) where T : IComponent =>
        htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await htmlRenderer.RenderComponentAsync<T>(parameters);
            return output.ToHtmlString();
        });
}