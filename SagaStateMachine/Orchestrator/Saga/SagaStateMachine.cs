using MassTransit;
using Messages;

namespace Orchestrator.Saga;

public class SagaStateMachine : MassTransitStateMachine<SagaState>
{
    public Event<StartSaga> StartSaga { get; }
    public Event<ActivityOneCompleted> ActivityOneCompleted { get; }
    public Event<ActivityTwoCompleted> ActivityTwoCompleted { get; }
    public Event<ActivityOneFailed> ActivityOneFailed { get; }
    public Event<ActivityTwoFailed> ActivityTwoFailed { get; }
    public Event<CompensateActivityOneCompleted> CompensateActivityOneCompleted { get; }
    public Event<CompensateActivityTwoCompleted> CompensateActivityTwoCompleted { get; }

    public State StateOne { get; }
    public State StateTwo { get; }
    public State Compensating { get; }
    public State Failed { get; }

    public SagaStateMachine()
    {
        InstanceState(p => p.CurrentState);

        Initially(
            When(StartSaga)
                .Then(context => context.Saga.CustomProperty = context.Message.CustomProperty)
                .PublishAsync(context =>
                    context.Init<StartActivityOne>(new { context.Saga.CorrelationId, context.Saga.CustomProperty })
                )
                .TransitionTo(StateOne)
        );

        During(StateOne,
            When(ActivityOneCompleted)
                .PublishAsync(context =>
                    context.Init<StartActivityTwo>(new { context.Saga.CorrelationId, context.Saga.CustomProperty })
                )
                .TransitionTo(StateTwo),
            When(ActivityOneFailed)
                .PublishAsync(context =>
                    context.Init<CompensateActivityOne>(new { context.Saga.CorrelationId })
                )
                .TransitionTo(Compensating));

        During(StateTwo,
            When(ActivityTwoCompleted)
                .Finalize(),
            When(ActivityTwoFailed)
                .PublishAsync(context =>
                    context.Init<CompensateActivityTwo>(new
                        { context.Saga.CorrelationId, context.Saga.CustomProperty })
                )
                .TransitionTo(Compensating));

        During(Compensating,
            When(CompensateActivityTwoCompleted)
                .PublishAsync(context =>
                    context.Init<CompensateActivityOne>(new { context.Saga.CorrelationId })
                ),
            When(CompensateActivityOneCompleted)
                .TransitionTo(Failed));
    }
}