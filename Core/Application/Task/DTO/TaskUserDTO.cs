namespace CrmCore.Core.Application.Task.DTO;

public record TaskUserDTO(
    int UserId,
    string? FirstName = "",
    string? LastName = "",
    string? MiddleName = ""
);