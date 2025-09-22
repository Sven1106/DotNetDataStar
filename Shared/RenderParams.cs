using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Shared;

public static class RenderParams
{
    public static ParameterView For<TComponent>(params IParam<TComponent>[] items)
        where TComponent : IComponent
    {
        var dict = new Dictionary<string, object?>(items.Length,
            StringComparer.OrdinalIgnoreCase);

        foreach (var item in items)
        {
            var (name, value) = item.Get();
            dict[name] = value;
        }

        return ParameterView.FromDictionary(dict);
    }

    public interface IParam<TComponent> where TComponent : IComponent
    {
        (string name, object? value) Get();
    }

    private sealed class Param<TComponent, V> : IParam<TComponent>
        where TComponent : IComponent
    {
        private readonly Expression<Func<TComponent, V>> _expr;
        private readonly V _value;

        public Param(Expression<Func<TComponent, V>> expr, V value)
        {
            _expr = expr;
            _value = value;
        }

        public (string name, object? value) Get()
            => (GetMemberName(_expr), _value);

        private static string GetMemberName(Expression<Func<TComponent, V>> expr)
        {
            var body = expr.Body is UnaryExpression u && u.NodeType == ExpressionType.Convert
                ? u.Operand
                : expr.Body;

            if (body is MemberExpression m) return m.Member.Name;

            throw new InvalidOperationException("Use x => x.Property");
        }
    }

    // Fabrik. Bemærk: V infereres fra value, TComponent fra For<...>
    public static IParam<TComponent> P<TComponent, V>(
        Expression<Func<TComponent, V>> prop,
        V value)
        where TComponent : IComponent
        => new Param<TComponent, V>(prop, value);
}