using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State currentState;

    public void changeState(State newState, Charactor chr)
    {
        if(currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.EnterState(chr);
        }
    }

    public void updateState()
    {
        if(currentState != null )
        {
            currentState.UpdateState();
        }
    }

    public void exitState()
    {
        currentState = null;
    }

    public State getState()
    {
        return currentState;
    }
}
