using MassTransit;
using Messages;

namespace ServiceB;

public class ActivityTwoConsumer : IConsumer<StartActivityTwo>
{
    public Task Consume(ConsumeContext<StartActivityTwo> context)
    {
        if (context.Message is { CustomProperty: "fail-two" })
        {
        }

        context.Publish(new ActivityTwoCompleted() { CorrelationId = context.CorrelationId!.Value });

        return Task.CompletedTask;
    }
}