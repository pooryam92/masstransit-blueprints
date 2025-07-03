using MassTransit;

namespace Messages;

[EntityName("compensate-activity-one-completed")]
public class CompensateActivityOneCompleted : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}