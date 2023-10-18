using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class State
{
    protected Charactor charactor;

    // State handers
    public abstract void EnterState(Charactor chr);

    public abstract void ExitState();

    public abstract void UpdateState();

    public abstract void ReSetState(Charactor chr); 
}
