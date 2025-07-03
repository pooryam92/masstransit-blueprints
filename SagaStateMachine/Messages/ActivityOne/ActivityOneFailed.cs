using MassTransit;

namespace Messages;

[EntityName("activity-one-failed")]
public class ActivityOneFailed : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}