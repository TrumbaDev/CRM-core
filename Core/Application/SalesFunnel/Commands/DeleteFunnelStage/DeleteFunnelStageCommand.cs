using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.DeleteFunnelStage;

public record DeleteFunnelStageCommand(int StageId): IRequest<int>;