using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class State
{
    protected Charactor charactor;


    public virtual void EnterState(Charactor chr)
    {
        Debug.Log(this);
        charactor = chr;
    }

    public virtual void ExitState() 
    {
        charactor = null;  
    }

    public virtual void UpdateState() { }
}
