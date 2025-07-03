using MassTransit;

namespace Messages;

[EntityName("compensate-activity-two")]
public class CompensateActivityTwo : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}