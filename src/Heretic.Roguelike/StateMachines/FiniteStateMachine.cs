using System.Collections.Generic;
using Heretic.Roguelike.StateMachines.EventArgs;

namespace Heretic.Roguelike.StateMachines;

public class FiniteStateMachine
{
    public IList<State> States { get; set; } = new List<State>();
    public State ActiveState { get; set; }

    private bool isMachineStarted = false;

    public FiniteStateMachine(State activeState)
    {
        this.ActiveState = activeState;
        this.States.Add(activeState);
    }

    public void AddState(State state)
    {
        if (!this.States.Contains(state))
        {
            this.States.Add(state);
        }
    }

    public void StartMachine(EnterEventArgs? eventArgs = null)
    {
        if (!this.isMachineStarted)
        {
            this.ActiveState.OnEnter(eventArgs ?? new EnterEventArgs());

            this.isMachineStarted = true;    
        }
    }

    public void UpdateMachine()
    {
        if (!this.isMachineStarted)
        {
            this.StartMachine();
            return;
        }
            
        this.ActiveState.OnUpdate(new UpdateEventArgs());

        foreach (var transition in this.ActiveState.Transitions)
        {
            if (transition.Evaluate())
            {
                this.ActiveState.OnExit(new ExitEventArgs());

                this.ActiveState = transition.ToState;
                this.ActiveState.OnEnter(new EnterEventArgs());
                    
                break;
            }
        }
    }    
}