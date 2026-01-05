namespace CrmCore.Core.Domain.User.Aggregate;

public record FullName
{
    public string First { get; }
    public string Last { get; }
    public string Middle { get; }

    public FullName(string first, string last, string middle = "")
    {
        if (string.IsNullOrWhiteSpace(first))
            throw new ArgumentException("First name is required");

        if (string.IsNullOrWhiteSpace(last))
            throw new ArgumentException("Last name is required");

        First = first;
        Last = last;
        Middle = middle;
    }
}