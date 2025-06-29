using MassTransit;

namespace Messages;

[EntityName("start-activity-one")]
public class StartActivityOne : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string? CustomProperty { get; set; }
}