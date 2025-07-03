using MassTransit;
using Messages;

namespace ServiceA;

public class CompensateActivityOneConsumer : IConsumer<CompensateActivityOne>
{
    public Task Consume(ConsumeContext<CompensateActivityOne> context)
    {
        Console.WriteLine("Compensating activity one");
        context.Publish(new CompensateActivityOneCompleted() { CorrelationId = context.CorrelationId!.Value });

        return Task.CompletedTask;
    }
}