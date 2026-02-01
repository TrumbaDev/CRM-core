using CrmCore.Core.Domain.Task.Repositories;
using MediatR;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Application.SalesFunnel.Commands.CreateSalesFunnel;

public class CreateSalesFunnelCommandHandler : IRequestHandler<CreateSalesFunnelCommand, int>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public CreateSalesFunnelCommandHandler(ISalesFunnelRepository salesFunnelRepo)
    {
        _salesFunnelRepo = salesFunnelRepo;
    }

    public async Task<int> Handle(CreateSalesFunnelCommand cmd, CancellationToken cancellationToken)
    {
        return await _salesFunnelRepo.AddAsync(
            SalesFunnelAggregate.Create(cmd.Name)
        );
    }
}