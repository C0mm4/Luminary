using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public override void EnterState(Charactor chr)
    {
        chr.AnimationPlay("DeadAnimation");
    }

    public override void ExitState()
    {
        GameManager.Resource.Destroy(charactor.gameObject);
    }

    public override void ReSetState(Charactor chr)
    {

    }

    public override void UpdateState()
    {

    }

}
