using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBuff : Buff
{
    public RockBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(5.2f);
        setTickTime(1f);
        
    }

    public override void durateEffect()
    {
        if(target != null)
        {

            if (currentTime - lastTickTime >= tickTime)
            {
                onTick();
                lastTickTime = currentTime;
            }
            target.element.Rock = true;
            base.durateEffect();

        }
        else
        {
            target = null;
            attacker = null;
        }
    }

    public override void onTick()
    {
        target.HPDecrease(1);
        base.onTick();
    }

    public override void endEffect()
    {
        target.element.Rock = false;
        base.endEffect();
    }
}
