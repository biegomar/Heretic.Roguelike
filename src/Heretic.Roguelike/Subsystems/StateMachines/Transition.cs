namespace Heretic.Roguelike.Subsystems.StateMachines;

public delegate bool EvaluateTransition();

public class Transition
{
    public State ToState { get; }
        
    private EvaluateTransition EvaluateTransition { get; }

    public Transition(EvaluateTransition evaluateTransition, State toState)
    {
        this.EvaluateTransition = evaluateTransition;
        this.ToState = toState;
    }

    public bool Evaluate()
    {
        return this.EvaluateTransition.Invoke();
    } 
}