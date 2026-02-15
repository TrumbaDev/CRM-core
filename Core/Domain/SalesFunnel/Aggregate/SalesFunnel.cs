using CrmCore.Core.Application.Common.Domains.Exceptions;
using CrmCore.Core.Application.Common.Exceptions;
using CrmCore.Core.Domain.SalesFunnel.ValueObjects;

namespace CrmCore.Core.Domain.SalesFunnel.Aggregate;

public class SalesFunnel
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public readonly List<FunnelStage> _funnelStages = [];
    public IReadOnlyCollection<FunnelStage> FunnelStages => _funnelStages.AsReadOnly();

    public static SalesFunnel Create(string name)
    {
        SalesFunnel funnel = new()
        {
            Name = name
        };
        funnel.AddDefaultStages();
        return funnel;
    }

    internal static SalesFunnel Regydrate(
        int id,
        string name,
        IEnumerable<FunnelStage> funnelStages
    )
    {
        SalesFunnel funnel = new()
        {
            Id = id,
            Name = name
        };

        funnel._funnelStages.AddRange(funnelStages);
        return funnel;
    }

    private void AddDefaultStages()
    {
        _funnelStages.Add(FunnelStage.Create(DefaultNewStage.Name, DefaultNewStage.Index));
        _funnelStages.Add(FunnelStage.Create(DefaultFailedStage.Name, DefaultFailedStage.Index));
        _funnelStages.Add(FunnelStage.Create(DefaultSuccessStage.Name, DefaultSuccessStage.Index));
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public void AddFunnelStage(FunnelStage stage)
    {
        if (stage.IndexNumber >= DefaultFailedStage.Index)
            throw new SalesFunnelException("Stage index number must be not greater than " + DefaultFailedStage.Index);

        if (stage.IndexNumber <= DefaultNewStage.Index)
            throw new SalesFunnelException("Stage index number must be greater than " + DefaultNewStage.Index);

        if (_funnelStages.Any(x => x.IndexNumber == stage.IndexNumber))
            throw new SalesFunnelException("Stage with index number " + stage.IndexNumber + " already exists");

        _funnelStages.Add(stage);
    }

    public void RenameStage(int stageId, string name)
    {
        FunnelStage stage = _funnelStages.FirstOrDefault(x => x.Id == stageId)
            ?? throw new NotFoundException("Undefined stage with id: " + stageId);

        stage.Rename(name);
    }

    public void DeleteStage(int stageId)
    {
        FunnelStage stage = _funnelStages.FirstOrDefault(x => x.Id == stageId)
            ?? throw new NotFoundException("Undefined stage with id: " + stageId);

        if (stage.IsDefaultStage())
            throw new SystemException("Cannot delete default stage");

        if (!_funnelStages.Remove(stage))
            throw new SalesFunnelException("Could not remove stage with id " + stageId);

        foreach (FunnelStage s in _funnelStages.Where(x => x.IndexNumber > stage.IndexNumber))
        {
            s.ShiftToLeft();
        }
    }
}