using CrmCore.Core.Application.User.DTO;
using MediatR;

namespace CrmCore.Core.Application.User.Queries.GetUserById;

public record GetUserByIdQuery(int Id): IRequest<UserDTO>;