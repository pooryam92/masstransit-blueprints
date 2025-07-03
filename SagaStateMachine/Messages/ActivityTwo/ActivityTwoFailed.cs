using MassTransit;

namespace Messages;

[EntityName("activity-two-failed")]
public class ActivityTwoFailed : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}