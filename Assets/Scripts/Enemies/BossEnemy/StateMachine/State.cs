using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public abstract class State
{
    public Action onEnter;
    public Action onExit;

    [SerializeReference]
    public List<Transition> transitions = new List<Transition>();

    //check if I actually use this anywhere
    protected Blackboard blackboard;

    public virtual void Enter()
    {
        onEnter?.Invoke();
    }

    public State NextState()
    {
        for (int i = 0; i < transitions.Count; i++)
        {
            if (transitions[i].condition())
            {
                return transitions[i].nextState;
            }
        }
        return null;
    }

    public virtual void Exit()
    {
        onExit?.Invoke();
    }

    public virtual void Step() { }
}
