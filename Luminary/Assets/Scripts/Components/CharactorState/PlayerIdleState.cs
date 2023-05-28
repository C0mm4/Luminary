using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public override void EnterState(Charactor chr)
    {
        Debug.Log(GameManager.inputManager);
        Debug.Log(GameManager.player);
        base.EnterState(chr);
        charactor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
