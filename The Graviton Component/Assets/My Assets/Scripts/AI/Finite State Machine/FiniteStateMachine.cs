using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FiniteStateMachine
{
    private Stack<FSMState> stateStack = new Stack<FSMState>();
    public delegate void FSMState(FiniteStateMachine fsm, GameObject gameobject);

    //Update is called once per frame
    public void Update (GameObject gameObject)
    {
        if(stateStack.Peek() != null)
        {
            stateStack.Peek().Invoke(this, gameObject);
        }
    }

    public void Push(FSMState state)
    {
        stateStack.Push(state);
    }

    public void Pop()
    {
        stateStack.Pop();
    }
}
