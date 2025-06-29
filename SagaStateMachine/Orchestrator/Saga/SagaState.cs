using MassTransit;

namespace Orchestrator.Saga;

public class SagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public required string CurrentState { get; set; }

    public string? CustomProperty { get; set; }

    // If using Optimistic concurrency, this property is required
    // public byte[] RowVersion { get; set; }
}