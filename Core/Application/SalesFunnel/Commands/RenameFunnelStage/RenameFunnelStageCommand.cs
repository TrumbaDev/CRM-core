using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.RenameFunnelStage;

public record RenameFunnelStageCommand(int FunnelId, int StageId, string Name): IRequest<int>;