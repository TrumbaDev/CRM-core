using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;
using SystemTask = System.Threading.Tasks.Task;

namespace CrmCore.Core.Domain.SalesFunnel.Repositories;

public interface ISalesFunnelRepository
{
    Task<SalesFunnelAggregate?> GetByIdAsync(int id);
    Task<List<SalesFunnelAggregate>> GetListAsync();
    Task<int> AddAsync(SalesFunnelAggregate salesFunnel);
    Task<int> DeleteAsync(int id);
    Task<int> RenameAsync(SalesFunnelAggregate salesFunnel);
    Task<int> AddStageAsync(SalesFunnelAggregate salesFunnel);
    Task<int> DeleteStageAsync(int id);
    Task<int> RenameStageAsync(SalesFunnelAggregate salesFunnel, int renamedStageId);
    SystemTask UpdateStagesIndexAsync(SalesFunnelAggregate salesFunnel);
}