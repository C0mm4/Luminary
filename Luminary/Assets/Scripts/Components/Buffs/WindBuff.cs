using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBuff : Buff
{
    public WindBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        target.status.pIncreaseSpeed -= 0.5f;
        target.calcStatus();
        setDurate(3f);
        
    }

    public override void durateEffect()
    {
        if(target != null)
        {
            target.status.element.Wind = true;
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
        target.status.element.Wind = false;
        target.status.pIncreaseSpeed += 0.5f;
        target.calcStatus();
        base.endEffect();
    }
}
