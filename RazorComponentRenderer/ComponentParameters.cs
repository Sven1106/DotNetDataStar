using Microsoft.AspNetCore.Components;

namespace RazorComponentRenderer;

// ReSharper disable once UnusedTypeParameter
public readonly record struct ComponentParameters<TComponent>(IReadOnlyDictionary<string, object?> Parameters) where TComponent : IComponent;