namespace CrmCore.Infrastructure.Data.SalesFunnel.Models;

public class SalesFunnelModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<FunnelStageModel> Stages { get; set; } = [];
    public DateTime CreateAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}