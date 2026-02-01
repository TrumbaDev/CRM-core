using CrmCore.Core.Application.SalesFunnel.DTO;
using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Queries.GetSalesFunnelById;

public record GetSalesFunnelByIdQuery(int Id): IRequest<SalesFunnelDTO>;