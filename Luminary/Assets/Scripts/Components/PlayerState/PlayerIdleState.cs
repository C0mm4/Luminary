using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        charactor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Debug.Log("Idle");
    }

    public override void UpdateState()
    {
        charactor.animator.Play("IdleAnimation");
//        charactor.AnimationPlay("IdleAnimation");
    }

    public override void ExitState()
    {
        charactor = null;
    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }
}
