using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace RazorComponentRenderer;

public class RazorComponentRendererService(HtmlRenderer htmlRenderer)
{
    public Task<string> RenderComponent<T>(ComponentParameters<T> componentParameters) where T : IComponent =>
        RenderComponent<T>(ParameterView.FromDictionary(
            componentParameters.Parameters as Dictionary<string, object?> ?? new Dictionary<string, object?>(componentParameters.Parameters)
        ));

    private Task<string> RenderComponent<T>(ParameterView parameterView) where T : IComponent =>
        htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var output = await htmlRenderer.RenderComponentAsync<T>(parameterView);
            return output.ToHtmlString();
        });
}