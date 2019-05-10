using System;
using System.Reflection;

namespace Npgsql
{
    internal static class CommandExtensioons
    {
        internal static void SetObjectResultTypes(this NpgsqlCommand @this, Type[] types)
        {
            var property = @this.GetType().GetProperty("ObjectResultTypes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            property.SetValue(@this, types);
        }
    }
}
