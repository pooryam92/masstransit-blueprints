using MassTransit;

namespace Messages;

[EntityName("compensate-activity-one")]
public class CompensateActivityOne : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}