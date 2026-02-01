using MediatR;

namespace CrmCore.Core.Application.SalesFunnel.Commands.CreateFunnelStage;

public record CreateFunnelStageCommand(int FunnelId, string Name, int IndexNumber): IRequest<int>;