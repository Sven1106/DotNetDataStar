using System.Text.Json;

namespace Empty;

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

public interface IGetAction
{
    string Get(ActionOptions? options = null);
}

public interface IPostAction
{
    string Post(ActionOptions? options = null);
}

public interface IPutAction
{
    string Put(ActionOptions? options = null);
}

public interface IPatchAction
{
    string Patch(ActionOptions? options = null);
}

public interface IDeleteAction
{
    string Delete(ActionOptions? options = null);
}

public interface IGetPostAction : IGetAction, IPostAction;

public interface IGetPutAction : IGetAction, IPutAction;

public interface IGetPatchAction : IGetAction, IPatchAction;

public interface IGetDeleteAction : IGetAction, IDeleteAction;

public interface IPostPutAction : IPostAction, IPutAction;

public interface IPostPatchAction : IPostAction, IPatchAction;

public interface IPostDeleteAction : IPostAction, IDeleteAction;

public interface IPutPatchAction : IPutAction, IPatchAction;

public interface IPutDeleteAction : IPutAction, IDeleteAction;

public interface IPatchDeleteAction : IPatchAction, IDeleteAction;

public interface IGetPostPutAction : IGetAction, IPostAction, IPutAction;

public interface IGetPostPatchAction : IGetAction, IPostAction, IPatchAction;

public interface IGetPostDeleteAction : IGetAction, IPostAction, IDeleteAction;

public interface IGetPutPatchAction : IGetAction, IPutAction, IPatchAction;

public interface IGetPutDeleteAction : IGetAction, IPutAction, IDeleteAction;

public interface IGetPatchDeleteAction : IGetAction, IPatchAction, IDeleteAction;

public interface IPostPutPatchAction : IPostAction, IPutAction, IPatchAction;

public interface IPostPutDeleteAction : IPostAction, IPutAction, IDeleteAction;

public interface IPostPatchDeleteAction : IPostAction, IPatchAction, IDeleteAction;

public interface IPutPatchDeleteAction : IPutAction, IPatchAction, IDeleteAction;

public interface IGetPostPutPatchAction : IGetAction, IPostAction, IPutAction, IPatchAction;

public interface IGetPostPutDeleteAction : IGetAction, IPostAction, IPutAction, IDeleteAction;

public interface IGetPostPatchDeleteAction : IGetAction, IPostAction, IPatchAction, IDeleteAction;

public interface IGetPutPatchDeleteAction : IGetAction, IPutAction, IPatchAction, IDeleteAction;

public interface IPostPutPatchDeleteAction : IPostAction, IPutAction, IPatchAction, IDeleteAction;

public interface IGetPostPutPatchDeleteAction : IGetAction, IPostAction, IPutAction, IPatchAction, IDeleteAction;

public class ApiAction(string url) : IGetPostAction, IGetPutAction, IGetPatchAction, IGetDeleteAction,
    IPostPutAction, IPostPatchAction, IPostDeleteAction, IPutPatchAction, IPutDeleteAction, IPatchDeleteAction,
    IGetPostPutAction, IGetPostPatchAction, IGetPostDeleteAction, IGetPutPatchAction, IGetPutDeleteAction, IGetPatchDeleteAction,
    IPostPutPatchAction, IPostPutDeleteAction, IPostPatchDeleteAction, IPutPatchDeleteAction,
    IGetPostPutPatchAction, IGetPostPutDeleteAction, IGetPostPatchDeleteAction, IGetPutPatchDeleteAction, IPostPutPatchDeleteAction,
    IGetPostPutPatchDeleteAction
{
    private static readonly JsonSerializerOptions CamelCaseOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private string DataStarAction(string method, ActionOptions? options)
    {
        if (options == null) return $"@{method}('{url}')";
        var optionsJson = JsonSerializer.Serialize(options, CamelCaseOptions);
        return $"@{method}('{url}', {optionsJson})";
    }

    public string Get(ActionOptions? options = null) => DataStarAction("get", options);
    public string Post(ActionOptions? options = null) => DataStarAction("post", options);

    public string Put(ActionOptions? options = null) => DataStarAction("put", options);

    public string Patch(ActionOptions? options = null) => DataStarAction("patch", options);

    public string Delete(ActionOptions? options = null) => DataStarAction("delete", options);
}

public static class ApiEndpoints
{
    public static IGetAction RemoveDate { get; } = new ApiAction("/removeDate");
    public static IPostAction ChangeOutput { get; } = new ApiAction("/changeOutput");
    public static IGetAction DisplayDate { get; } = new ApiAction("/displayDate");
}