using MassTransit;

namespace Messages;

public class StepOne : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}