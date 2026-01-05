namespace CrmCore.Core.Domain.User.Aggregate;

public record PhoneNumber
{
    public string Value { get; }

    public PhoneNumber(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone number is required");

        Value = phone;
    }
}