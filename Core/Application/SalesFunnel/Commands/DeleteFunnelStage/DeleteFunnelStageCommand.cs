using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.DeleteFunnelStage;

public record DeleteFunnelStageCommand(int FunnelId, int StageId): IRequest<int>;