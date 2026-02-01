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
        _funnelStages.Add(stage);
    }

    public void RenameStage(int stageId, string name)
    {
        _funnelStages.First(x => x.Id == stageId).Rename(name);
    }
}