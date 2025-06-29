using MassTransit;

namespace Messages;

[EntityName("start-activity-two")]
public class StartActivityTwo : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string? CustomProperty { get; set; }
}