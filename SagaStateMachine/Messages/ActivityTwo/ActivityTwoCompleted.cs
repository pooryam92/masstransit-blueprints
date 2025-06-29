using MassTransit;

namespace Messages;

[EntityName("activity-two-completed")]
public class ActivityTwoCompleted : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}