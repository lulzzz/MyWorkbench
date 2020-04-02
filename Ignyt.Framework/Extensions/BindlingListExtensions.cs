using Ignyt.Framework.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;

public static class BindlingListExtensions
{
    public static string ToDelimitedString<T>(this BindingList<T> source, string delimiter) where T : IKeyValuePair<string,string>
    {
        return string.Join(delimiter, source.Select(g => g.KeyValuePair.Key.ToString()).ToArray());
    }
}
