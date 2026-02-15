using CrmCore.Core.Domain.SalesFunnel.ValueObjects;

namespace CrmCore.Core.Domain.SalesFunnel.Aggregate;

public class FunnelStage
{
    public int Id { get; private set; }
    public string Name { get; private set; } = String.Empty;
    public int IndexNumber { get; private set; }

    public static FunnelStage Create(string name, int indexNumber)
    {
        return new FunnelStage
        {
            Name = name,
            IndexNumber = indexNumber
        };
    }

    internal static FunnelStage Rehydrate(int id, string name, int indexNumber)
    {
        return new FunnelStage
        {
            Id = id,
            Name = name,
            IndexNumber = indexNumber
        };
    }

    public void Rename(string name)
    {
        Name = name;
    }

    public bool IsDefaultStage()
    {
        return IndexNumber == DefaultNewStage.Index ||
            IndexNumber == DefaultFailedStage.Index ||
            IndexNumber == DefaultSuccessStage.Index;
    }

    public void ShiftToLeft()
    {
        if (IsDefaultStage()) return;

        IndexNumber--;
    }
}