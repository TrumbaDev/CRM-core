using CrmCore.Core.Domain.SalesFunnel.Aggregate;
using CrmCore.Core.Domain.SalesFunnel.Factories;
using CrmCore.Core.Domain.SalesFunnel.Repositories;
using CrmCore.Infrastructure.Data.SalesFunnel.Models;
using Microsoft.EntityFrameworkCore;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;
using SystemTask = System.Threading.Tasks.Task;

namespace CrmCore.Infrastructure.Data.SalesFunnel.Repositories;

public class SalesFunnelRepository : ISalesFunnelRepository
{
    private readonly SalesFunnelDbContext _context;
    private readonly SalesFunnelFactory _factory;

    public SalesFunnelRepository(SalesFunnelDbContext context, SalesFunnelFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    public async Task<SalesFunnelAggregate?> GetByIdAsync(int id)
    {
        SalesFunnelModel? model = await _context.SalesFunnel
            .Include(t => t.Stages)
            .FirstOrDefaultAsync(x => x.Id == id);
        return model is null ? null : _factory.Rehydrate(model);
    }

    public async Task<List<SalesFunnelAggregate>> GetListAsync()
    {
        List<SalesFunnelModel> models = await _context.SalesFunnel
            .Include(t => t.Stages)
            .ToListAsync();
        return models.Select(_factory.Rehydrate).ToList();
    }

    public async Task<int> AddAsync(SalesFunnelAggregate salesFunnel)
    {
        SalesFunnelModel model = new()
        {
            Name = salesFunnel.Name,
        };

        foreach (FunnelStage s in salesFunnel.FunnelStages)
        {
            model.Stages.Add(new FunnelStageModel
            {
                Name = s.Name,
                IndexNumber = s.IndexNumber
            });
        }

        _context.SalesFunnel.Add(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _context.SalesFunnel
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public Task<int> RenameAsync(SalesFunnelAggregate salesFunnel)
    {
        throw new Exception();
    }

    public Task<int> AddStageAsync(SalesFunnelAggregate salesFunnel)
    {
        throw new Exception();
    }

    public Task<int> DeleteStageAsync(int id)
    {
        throw new Exception();
    }

    public Task<int> RenameStageAsync(SalesFunnelAggregate salesFunnel, int renamedStageId)
    {
        throw new Exception();
    }

    public SystemTask UpdateStagesIndexAsync(SalesFunnelAggregate salesFunnel)
    {
        throw new Exception();
    }
}