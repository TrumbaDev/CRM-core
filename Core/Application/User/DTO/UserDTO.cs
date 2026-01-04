namespace CrmCore.Application.User.DTO;

public record UserDTO(
    int Id,
    string FirstName,
    string LastName,
    string MiddleName,
    string Email,
    string Phone
);