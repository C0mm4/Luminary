using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobIdleState : State
{
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
    }

    public override void UpdateState()
    {
        Debug.Log("Idle");
    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }

    public override void ExitState()
    {

    }

}
