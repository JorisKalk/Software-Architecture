using UnityEngine;

public abstract class FSM : State
{
    protected State currentState;

    public override void Step()
    {
        base.Step();
        currentState.Step();
        if (currentState.NextState() != null)
        {
            State nextState = currentState.NextState();
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }
    }
}
