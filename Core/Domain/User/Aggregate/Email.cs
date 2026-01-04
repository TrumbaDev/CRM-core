namespace CrmCore.Domain.User.Aggregate;

public record Email
{
    public string Value { get; }

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Invalid email");

        Value = email;
    }
}