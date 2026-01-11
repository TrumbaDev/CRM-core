using CrmCore.Core.Application.Task.DTO;
using MediatR;

namespace CrmCore.Core.Application.Task.Queries.GetTaskById;

public record GetTaskByIdQuery(int Id): IRequest<TaskDTO?>;