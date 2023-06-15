using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : Buff
{
    public FireBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(5.2f);
        setTickTime(1f);

        if (target.status.element.Ice == true)
        {
            target.status.element.FireIce = true;
            target.status.pGetDMG += 0.30f;
            target.calcStatus();
        }

        if (target.status.element.Wind == true)
        {
            target.status.element.FireWind = true;
            durate += 3;
        }

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
            target.status.element.Fire = true;
            base.durateEffect();

            if (target.status.element.FireIce == true && target.status.element.Ice == false)
            {
                target.status.element.FireIce = false;
                target.status.pGetDMG -= 0.30f;
                target.calcStatus();
            }
            if (target.status.element.FireWind == true && target.status.element.Wind == false)
            {
                target.status.element.FireWind = false;
                target.calcStatus();
            }
        }
        else
        {
            target = null;
            attacker = null;
        }
    }

    public override void onTick()
    {
        if (target.status.element.FireWind == true)
        {
            target.HPDecrease(2);
        }
        else
        {
            target.HPDecrease(1);
        }

        base.onTick();
    }

    public override void endEffect()
    {
        if (target.status.element.FireIce == true)
        {
            target.status.element.FireIce = false;
            target.status.pGetDMG -= 0.30f;
            target.calcStatus();
        }

        target.status.element.Fire = false;
        base.endEffect();
    }
}
