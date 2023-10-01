using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobIdleState : State
{
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        charactor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public override void UpdateState()
    {

    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }

    public override void ExitState()
    {

    }

}
