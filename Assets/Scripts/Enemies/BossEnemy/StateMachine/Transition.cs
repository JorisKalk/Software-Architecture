using System;
using UnityEngine;

[Serializable]
public class Transition
{
    public Func<bool> condition;

    public State nextState;

    ///<param name="pCondition">The condition function to evaluate.</param>
    ///<param name="pNextState">The next state to transition to if contion is true.</param>
    public Transition(Func<bool> pCondition, State pNextState)
    {
        condition = pCondition;
        nextState = pNextState;
    }
}
