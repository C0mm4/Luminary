using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : Buff
{
    public FireBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(5.2f);
        setTickTime(1f);
        
    }

    public override void durateEffect()
    {
        if (currentTime - lastTickTime >= tickTime)
        {
            onTick();
            lastTickTime = currentTime;
        }
        target.element.Fire = true;
        base.durateEffect();
        
    }

    public override void onTick()
    {
        target.HPDecrease(1);
        base.onTick();
    }

    public override void endEffect()
    {
        target.element.Fire = false;
        base.endEffect();
    }
}
