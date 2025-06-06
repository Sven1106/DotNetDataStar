using System.Text.Json;

namespace Empty;

public static class ApiEndpoints
{
    public abstract record ApiEndpointDescriptor(string Pattern);

    public sealed record DisplayDateEndpoint() : ApiEndpointDescriptor("/displayDate");

    public sealed record RemoveDateEndpoint() : ApiEndpointDescriptor("/removeDate");

    public sealed record ChangeOutputEndpoint() : ApiEndpointDescriptor("/changeOutput");

    public sealed record Examples_ClickToEdit_Contact_Id_IntEndpoint(int Id) : ApiEndpointDescriptor($"/examples/click_to_edit/contact/{Id}");
    public static string Get(Examples_ClickToEdit_Contact_Id_IntEndpoint endpoint, ActionOptions? options = null) => DataStarAction(HttpMethod.Get, endpoint.Pattern, options);
    public static string Get(RemoveDateEndpoint endpoint, ActionOptions? options = null) => DataStarAction(HttpMethod.Get, endpoint.Pattern, options);
    public static string Get(DisplayDateEndpoint endpoint, ActionOptions? options = null) => DataStarAction(HttpMethod.Get, endpoint.Pattern, options);
    public static string Post(ChangeOutputEndpoint endpoint, ActionOptions? options = null) => DataStarAction(HttpMethod.Post, endpoint.Pattern, options);

    public record ActionOptions(
        string? ContentType = null,
        bool? IncludeLocal = null,
        string? Selector = null,
        Dictionary<string, string>? Headers = null,
        bool? OpenWhenHidden = null,
        int? RetryInterval = null,
        int? RetryScaler = null,
        int? RetryMaxWaitMs = null,
        int? RetryMaxCount = null
    );

    private static readonly JsonSerializerOptions CamelCaseOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private enum HttpMethod
    {
        Get,
        Post,
        Put,
        Delete
    }

    private static string DataStarAction(HttpMethod method, string url, ActionOptions? options)
    {
        var methodName = method.ToString().ToLowerInvariant();
        if (options == null) return $"@{methodName}('{url}')";
        var optionsJson = JsonSerializer.Serialize(options, CamelCaseOptions);
        return $"@{methodName}('{url}', {optionsJson})";
    }
}