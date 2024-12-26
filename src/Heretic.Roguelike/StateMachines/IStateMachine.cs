using System.Collections.Generic;
using Heretic.Roguelike.StateMachines.EventArgs;

namespace Heretic.Roguelike.StateMachines;

public interface IStateMachine
{
    IList<State> States { get; set; }
    State ActiveState { get; set; }
    void AddState(State state);
    void StartMachine(EnterEventArgs? eventArgs = null);
    void UpdateMachine();
}