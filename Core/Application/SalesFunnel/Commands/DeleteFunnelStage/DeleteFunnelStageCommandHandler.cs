using CrmCore.Core.Domain.Task.Repositories;
using MediatR;

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
        return await _salesFunnelRepo.DeleteStageAsync(cmd.StageId);
    }
}