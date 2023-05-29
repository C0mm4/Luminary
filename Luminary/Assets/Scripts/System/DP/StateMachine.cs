using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Stack<State> stateStack = new Stack<State>();
    private State currentState = null;

    private Charactor target;

    public StateMachine(Charactor chr)
    {
        target = chr;
        
    }


    public void changeState(State newState)
    {

        if (currentState != null) {
            if (GameManager.FSM.getList(currentState.GetType().Name).Contains(newState.GetType().Name))
            {
                Debug.Log(newState);
                
                if (currentState.GetType().Name != newState.GetType().Name)
                {
                    // Save Previous State
                    stateStack.Push(currentState);

                    // change New State
                    currentState = newState;

                    // State Enter Logic Process
                    currentState.EnterState(target);
                }
                // if currentState is Equal State
                else
                {
                    // change New State
                    currentState = newState;

                    // State Enter Logic Process
                    currentState.EnterState(target);
                }
            }
        }
        else
        {
            Debug.Log(newState);
            currentState = newState;

            currentState.EnterState(target);
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
        currentState.ExitState();
        if (stateStack.Count > 0)
        {
            currentState = stateStack.Pop();

            currentState.ReSetState();
        }
    }

    public string getStateStr()
    {
        return currentState.GetType().Name;
    }

    public State getState()
    {
        return currentState;
    }
}
