using Microsoft.AspNetCore.Components;

namespace BlazorRenderer;

public readonly record struct ComponentParameters<TComponent>(IReadOnlyDictionary<string, object?> Parameters) where TComponent : IComponent;