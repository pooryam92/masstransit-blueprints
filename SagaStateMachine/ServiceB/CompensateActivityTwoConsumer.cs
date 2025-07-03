using MassTransit;
using Messages;

namespace ServiceB;

public class CompensateActivityTwoConsumer : IConsumer<CompensateActivityTwo>
{
    public Task Consume(ConsumeContext<CompensateActivityTwo> context)
    {
        Console.WriteLine("Compensating activity two");
        context.Publish(new CompensateActivityTwoCompleted() { CorrelationId = context.CorrelationId!.Value });

        return Task.CompletedTask;
    }
}