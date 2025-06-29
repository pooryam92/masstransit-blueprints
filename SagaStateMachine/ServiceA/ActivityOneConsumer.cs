using MassTransit;
using Messages;

namespace ServiceA;

public class ActivityOneConsumer : IConsumer<StartActivityOne>
{
    public Task Consume(ConsumeContext<StartActivityOne> context)
    {
        if (context.Message is { CustomProperty: "fail-one" })
        {
        }

        context.Publish(new ActivityOneCompleted() { CorrelationId = context.CorrelationId!.Value });

        return Task.CompletedTask;
    }
}