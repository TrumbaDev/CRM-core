using CrmCore.Core.Application.SalesFunnel.DTO;
using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Queries.GetSalesFunnelList;

public record GetSalesFunnelListQuery(): IRequest<List<SalesFunnelDTO>>;