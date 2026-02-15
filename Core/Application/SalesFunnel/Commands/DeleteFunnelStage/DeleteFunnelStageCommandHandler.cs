using CrmCore.Core.Application.Common.Exceptions;
using CrmCore.Core.Domain.SalesFunnel.Repositories;
using MediatR;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Application.SalesFunnel.Commands.DeleteFunnelStage;

public class DeleteFunnelStageCommandHandler : IRequestHandler<DeleteFunnelStageCommand, int>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public DeleteFunnelStageCommandHandler(ISalesFunnelRepository salesFunnelRepo)
    {
        _salesFunnelRepo = salesFunnelRepo;
    }

    public async Task<int> Handle(DeleteFunnelStageCommand cmd, CancellationToken cancellationToken)
    {
        SalesFunnelAggregate salesFunnel = await _salesFunnelRepo.GetByIdAsync(cmd.FunnelId)
            ?? throw new NotFoundException("Sales funnel not found");
        salesFunnel.DeleteStage(cmd.StageId);

        int deletedIndex = await _salesFunnelRepo.DeleteStageAsync(cmd.StageId);
        await _salesFunnelRepo.UpdateStagesIndexAsync(salesFunnel);
        return deletedIndex;
    }
}