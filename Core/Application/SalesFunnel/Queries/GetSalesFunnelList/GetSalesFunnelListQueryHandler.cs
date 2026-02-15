using CrmCore.Core.Application.SalesFunnel.DTO;
using CrmCore.Core.Domain.SalesFunnel.Repositories;
using MediatR;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Application.SalesFunnel.Queries.GetSalesFunnelList;

public class GetSalesFunnelListQueryHandler : IRequestHandler<GetSalesFunnelListQuery, List<SalesFunnelDTO>>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public GetSalesFunnelListQueryHandler(ISalesFunnelRepository salesFunnelRepository)
    {
        _salesFunnelRepo = salesFunnelRepository;
    }

    public async Task<List<SalesFunnelDTO>> Handle(GetSalesFunnelListQuery query, CancellationToken cancellationToken)
    {
        List<SalesFunnelAggregate> salesFunnels = await _salesFunnelRepo.GetListAsync();
        return salesFunnels.Select(
            salesFunnel => new SalesFunnelDTO(
                salesFunnel.Id,
                salesFunnel.Name
            )).ToList();
    }
}