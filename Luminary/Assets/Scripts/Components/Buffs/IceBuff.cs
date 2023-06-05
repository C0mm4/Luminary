using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBuff : Buff
{
    public IceBuff(Charactor tar, Charactor atk) : base(tar, atk)
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
            target.element.Ice = true;
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
        target.element.Ice = false;
        base.endEffect();
    }
}
