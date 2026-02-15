using CrmCore.Core.Application.SalesFunnel.DTO;
using CrmCore.Core.Domain.SalesFunnel.Repositories;
using MediatR;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Application.SalesFunnel.Queries.GetSalesFunnelById;

public class GetSalesFunnelByIdQueryHandler : IRequestHandler<GetSalesFunnelByIdQuery, SalesFunnelDTO?>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public GetSalesFunnelByIdQueryHandler(ISalesFunnelRepository salesFunnelRepo)
    {
        _salesFunnelRepo = salesFunnelRepo;
    }

    public async Task<SalesFunnelDTO?> Handle(GetSalesFunnelByIdQuery query, CancellationToken cancellationToken)
    {
        SalesFunnelAggregate? salesFunnel = await _salesFunnelRepo.GetByIdAsync(query.Id);
        if (salesFunnel is null) return null;

        return new SalesFunnelDTO(
            salesFunnel.Id,
            salesFunnel.Name
        );
    }
}