using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.RenameSalesFunnel;

public record RenameSalesFunnelCommand(int Id, string Name) : IRequest<int>;