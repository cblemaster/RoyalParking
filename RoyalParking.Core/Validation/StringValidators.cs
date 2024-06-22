using System.Text.RegularExpressions;

namespace RoyalParking.Core.Validation;

internal static class StringValidators
{
    internal static bool IsStringLengthValid(this string value, bool isRequired, uint maxLength)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            uint minLength = isRequired ? (uint)1 : 0;
            return value.Length >= minLength && value.Length <= maxLength;
        }
        return false;
    }

    internal static bool IsStringAllNumerals(this string value)
    {
        return value.ToCharArray().All(c => Regex.Match(c.ToString(), "[0-9]").Success);
    }
}
