using System.Collections.Generic;
using Heretic.Roguelike.Subsystems.StateMachines.EventArgs;

namespace Heretic.Roguelike.Subsystems.StateMachines;

public interface IStateMachine
{
    IList<State> States { get; set; }
    State ActiveState { get; set; }
    void AddState(State state);
    void StartMachine(EnterEventArgs? eventArgs = null);
    void UpdateMachine();
}