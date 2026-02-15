using CrmCore.Core.Domain.SalesFunnel.Repositories;
using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.DeleteSalesFunnel;

public class DeleteSalesFunnelCommandHandler : IRequestHandler<DeleteSalesFunnelCommand, int>
{
    private readonly ISalesFunnelRepository _salesFunnelRepo;

    public DeleteSalesFunnelCommandHandler(ISalesFunnelRepository salesFunnelRepository)
    {
        _salesFunnelRepo = salesFunnelRepository;
    }

    public async Task<int> Handle(DeleteSalesFunnelCommand cmd, CancellationToken cancellationToken)
    {
        return await _salesFunnelRepo.DeleteAsync(cmd.Id);
    }
}