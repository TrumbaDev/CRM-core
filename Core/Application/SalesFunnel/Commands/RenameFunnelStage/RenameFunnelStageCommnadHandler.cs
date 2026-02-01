using CrmCore.Core.Application.Common.Exceptions;
using CrmCore.Core.Domain.Task.Repositories;
using MediatR;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Application.SalesFunnel.Commands.RenameFunnelStage;

public class RenameFunnelStageCommandHandler: IRequestHandler<RenameFunnelStageCommand, int>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public RenameFunnelStageCommandHandler(ISalesFunnelRepository salesFunnelRepo)
    {
        _salesFunnelRepo = salesFunnelRepo;
    }

    public async Task<int> Handle(RenameFunnelStageCommand cmd, CancellationToken cancellationToken)
    {
        SalesFunnelAggregate salesFunnel = await _salesFunnelRepo.GetByIdAsync(cmd.FunnelId)
            ?? throw new NotFoundException("Sales funnel not found");
        salesFunnel.RenameStage(cmd.StageId, cmd.Name);
        return await _salesFunnelRepo.RenameStageAsync(salesFunnel);
    }
}