using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBuff : Buff
{
    public RockBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(2f);
        
    }

    public override void durateEffect()
    {
        if(target != null)
        {
            target.status.element.Rock = true;
            target.status.pIncreaseDMG -= 0.03f;
            target.calcStatus();
            base.durateEffect();

        }
        else
        {
            target = null;
            attacker = null;
        }
    }

 
    public override void endEffect()
    {
        target.status.element.Rock = false;
        target.status.pIncreaseDMG += 0.03f;
        target.calcStatus();
        base.endEffect();
    }
}
