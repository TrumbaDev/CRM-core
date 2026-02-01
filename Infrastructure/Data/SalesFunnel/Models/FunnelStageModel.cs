namespace CrmCore.Infrastructure.Data.SalesFunnel.Models;

public class FunnelStageModel
{
    public int Id { get; set; }
    public int FunnelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int IndexNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}