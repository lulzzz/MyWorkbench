using System;

/// <summary>
/// 	Extension methods for byte-Arrays
/// </summary>
public static class GuidExtensions
{
    public static bool IsGuid(this string GuidString)
    {
        return Guid.TryParse(GuidString, out Guid x);
    }
}
