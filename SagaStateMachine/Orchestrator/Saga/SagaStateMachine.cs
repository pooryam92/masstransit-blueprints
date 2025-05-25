using MassTransit;
using Messages;

namespace Orchestrator.Saga;

public class SagaStateMachine : MassTransitStateMachine<SagaState>
{
    public Event<StartSaga> StartSaga { get; private set; }

    public State SagaStarted { get; set; }

    public SagaStateMachine()
    {
        InstanceState(p => p.CurrentState);

        Initially(
            When(StartSaga)
                .TransitionTo(SagaStarted)
        );
    }
}