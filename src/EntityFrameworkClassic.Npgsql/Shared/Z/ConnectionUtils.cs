using System;
using System.Reflection;
using Npgsql;

namespace Npgsql
{
    internal static class ConnectionExtensions
    {
        internal static NpgsqlConnectionStringBuilder GetSettings(this NpgsqlConnection @this)
        {
            var property = @this.GetType().GetProperty("Settings", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return (NpgsqlConnectionStringBuilder)property.GetValue(@this, null);
            return null;
        }
    }
}
