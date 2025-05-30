using MassTransit;
using Messages;

namespace Orchestrator.Saga;

public class SagaStateMachine : MassTransitStateMachine<SagaState>
{
    public Event<StartSaga> StartSaga { get; private set; }

    public State StateOne { get; set; }

    public SagaStateMachine()
    {
        InstanceState(p => p.CurrentState);

        Initially(
            When(StartSaga)
                .Then(context => context.Saga.CustomProperty = context.Message.CustomProperty)
                .PublishAsync(context => context.Init<StepOne>(new { context.Saga.CorrelationId }))
                .TransitionTo(StateOne)
        );
    }
}