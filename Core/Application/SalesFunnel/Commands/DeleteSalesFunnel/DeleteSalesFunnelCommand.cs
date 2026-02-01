using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.DeleteSalesFunnel;

public record DeleteSalesFunnelCommand(int Id): IRequest<int>;