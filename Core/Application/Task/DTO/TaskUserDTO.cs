namespace CrmCore.Core.Application.Task.DTO;

public record TaskUserDTO(
    int UserId,
    string FisrtName,
    string LastName,
    string MiddleName
);