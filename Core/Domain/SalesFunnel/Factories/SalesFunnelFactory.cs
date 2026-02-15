using CrmCore.Core.Domain.SalesFunnel.Aggregate;
using CrmCore.Infrastructure.Data.SalesFunnel.Models;
using SalesFunnelAggregate = CrmCore.Core.Domain.SalesFunnel.Aggregate.SalesFunnel;

namespace CrmCore.Core.Domain.SalesFunnel.Factories;

public class SalesFunnelFactory
{
    public SalesFunnelAggregate Rehydrate(SalesFunnelModel model)
    {
        List<FunnelStage> stages = model.Stages
            .Select(s => FunnelStage.Rehydrate(
                s.Id,
                s.Name,
                s.IndexNumber
            )).ToList();

        return SalesFunnelAggregate.Regydrate(
            model.Id,
            model.Name,
            stages
        );
    }
}