using MassTransit;

namespace Messages;

[EntityName("compensate-activity-two-completed")]
public class CompensateActivityTwoCompleted : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}