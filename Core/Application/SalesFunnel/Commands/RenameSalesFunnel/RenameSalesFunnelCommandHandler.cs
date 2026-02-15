using CrmCore.Core.Application.Common.Exceptions;
using CrmCore.Core.Domain.SalesFunnel.Repositories;
using MediatR;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Application.SalesFunnel.Commands.RenameSalesFunnel;

public class RenameSalesFunnelCommandHandler : IRequestHandler<RenameSalesFunnelCommand, int>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public RenameSalesFunnelCommandHandler(ISalesFunnelRepository salesFunnelRepository)
    {
        _salesFunnelRepo = salesFunnelRepository;
    }

    public async Task<int> Handle(RenameSalesFunnelCommand cmd, CancellationToken cancellationToken)
    {
        SalesFunnelAggregate salesFunnel = await _salesFunnelRepo.GetByIdAsync(cmd.Id)
            ?? throw new NotFoundException("SalesFunnel not found");

        salesFunnel.Rename(cmd.Name);
        return await _salesFunnelRepo.RenameAsync(salesFunnel);
    }
}