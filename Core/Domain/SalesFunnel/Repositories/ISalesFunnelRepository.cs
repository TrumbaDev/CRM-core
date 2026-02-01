using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Domain.Task.Repositories;

public interface ISalesFunnelRepository
{
    Task<SalesFunnelAggregate?> GetByIdAsync(int id);
    Task<List<SalesFunnelAggregate>> GetListAsync();
    Task<int> AddAsync(SalesFunnelAggregate salesFunnel);
    Task<int> DeleteAsync(int id);
    Task<int> RenameAsync(SalesFunnelAggregate salesFunnel);
    Task<int> AddStageAsync(SalesFunnelAggregate salesFunnel);
    Task<int> DeleteStageAsync(int id);
    Task<int> RenameStageAsync(SalesFunnelAggregate salesFunnel);
}