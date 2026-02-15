using CrmCore.Core.Application.Common.Exceptions;
using CrmCore.Core.Domain.SalesFunnel.Aggregate;
using CrmCore.Core.Domain.SalesFunnel.Repositories;
using MediatR;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Application.SalesFunnel.Commands.CreateFunnelStage;

public class CreateFunnelStageCommandHandler : IRequestHandler<CreateFunnelStageCommand, int>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public CreateFunnelStageCommandHandler(ISalesFunnelRepository salesFunnelRepo)
    {
        _salesFunnelRepo = salesFunnelRepo;
    }

    public async Task<int> Handle(CreateFunnelStageCommand cmd, CancellationToken cancellationToken)
    {
        SalesFunnelAggregate salesFunnel = await _salesFunnelRepo.GetByIdAsync(cmd.FunnelId)
            ?? throw new NotFoundException("Sales funnel not found");

        salesFunnel.AddFunnelStage(FunnelStage.Create(cmd.Name, cmd.IndexNumber));
        return await _salesFunnelRepo.AddStageAsync(salesFunnel);
    }
}