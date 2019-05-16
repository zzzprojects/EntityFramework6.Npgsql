using System;
using System.Linq.Expressions;
using System.Reflection;
using Npgsql;

namespace Npgsql
{
    internal static class ConnectionExtensions
    {
        /// <summary>The get settings delegate.</summary>
        private static readonly Lazy<Func<NpgsqlConnection, NpgsqlConnectionStringBuilder>> _getSettingsDelegate = new Lazy<Func<NpgsqlConnection, NpgsqlConnectionStringBuilder>>(() =>
        {
            // parameter
            var parameter1 = Expression.Parameter(typeof(NpgsqlConnection));

            // expression
            var settingsProperty = typeof(NpgsqlConnection).GetProperty("Settings", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var expression = Expression.Property(parameter1, settingsProperty);

            // compile
            var compiled = Expression.Lambda<Func<NpgsqlConnection, NpgsqlConnectionStringBuilder>>(expression, parameter1).Compile();
            return compiled;
        });

        /// <summary>A NpgsqlConnection extension method that gets the settings.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The settings.</returns>
        internal static NpgsqlConnectionStringBuilder GetSettings(this NpgsqlConnection @this)
        {
            return _getSettingsDelegate.Value(@this);
        }
    }
}
