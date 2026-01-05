namespace CrmCore.Core.Application.Common.Services;

public static class PhoneFormatter
{
    public static string Format(string raw) => new([.. raw.Where(char.IsDigit)]);
}