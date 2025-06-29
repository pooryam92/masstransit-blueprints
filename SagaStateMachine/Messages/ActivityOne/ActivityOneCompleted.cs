using MassTransit;

namespace Messages;

[EntityName("activity-one-completed")]
public class ActivityOneCompleted : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}