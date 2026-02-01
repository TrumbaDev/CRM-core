using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.CreateSalesFunnel;

public record CreateSalesFunnelCommand(
    string Name
): IRequest<int>;