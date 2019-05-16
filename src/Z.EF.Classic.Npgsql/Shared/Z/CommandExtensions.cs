using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Npgsql
{
    internal static class CommandExtensions
    {
        /// <summary>The get settings delegate.</summary>
        private static readonly Lazy<Action<NpgsqlCommand, Type[]>> _setObjectResultTypesDelegate = new Lazy<Action<NpgsqlCommand, Type[]>>(() =>
        {
            // parameter
            var parameter1 = Expression.Parameter(typeof(NpgsqlCommand));
            var parameter2 = Expression.Parameter(typeof(Type[]));

            // expression
            var settingsProperty = typeof(NpgsqlCommand).GetProperty("ObjectResultTypes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var expression = Expression.Property(parameter1, settingsProperty, parameter2);

            // compile
            var compiled = Expression.Lambda<Action<NpgsqlCommand, Type[]>>(expression, parameter1, parameter2).Compile();
            return compiled;
        });

        internal static void SetObjectResultTypes(this NpgsqlCommand @this, Type[] types)
        {
            _setObjectResultTypesDelegate.Value(@this, types);
        }
    }
}
