using MassTransit;
using Messages;

namespace Orchestrator.Saga;

public class SagaStateMachine : MassTransitStateMachine<SagaState>
{
    public Event<StartSaga> StartSaga { get; }
    public Event<ActivityOneCompleted> ActivityOneCompleted { get; }
    public Event<ActivityTwoCompleted> ActivityTwoCompleted { get; }

    public State StateOne { get; }
    public State StateTwo { get; }
    public State Completed { get; }

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
                .TransitionTo(StateTwo));

        During(StateTwo,
            When(ActivityTwoCompleted)
                .TransitionTo(Completed));
    }
}