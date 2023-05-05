using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobIdleState : State
{
    public override void EnterState(Charactor chr)
    {
        base.EnterState(chr);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("Idle");
    }


}
