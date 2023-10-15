using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobATKState : State
{
    int index;

    public MobATKState(int index)
    {
        this.index = index;
    }
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
    }

    public override void ExitState()
    {
        charactor = null;
    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }

    public override void UpdateState()
    {
        charactor.AnimationPlay("AttackAnimation " + index);
    }
}
