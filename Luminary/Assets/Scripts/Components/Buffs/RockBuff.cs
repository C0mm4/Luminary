using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBuff : Buff
{
    public RockBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        target.status.pIncreaseDMG -= 0.03f;
        target.calcStatus();
        setDurate(2f);

        if (target.status.element.Ice == true)
        {
            target.status.element.IceRock = true;
            target.status.pIncreaseSpeed -= 1.0f;
            target.calcStatus();
        }

        if (target.status.element.Wind == true)
        {
            target.status.element.WindRock = true;
            target.status.pGetDMG += 0.3f;
            target.calcStatus();
        }

    }

    public override void durateEffect()
    {
        if(target != null)
        {
            target.status.element.Rock = true;
            base.durateEffect();

            if (target.status.element.IceRock == true && target.status.element.Ice == false)
            {
                target.status.element.IceRock = false;
                target.status.pIncreaseSpeed += 1.0f;
                target.calcStatus();
            }

            if (target.status.element.WindRock == true && target.status.element.Wind == false)
            {
                target.status.element.WindRock = false;
                target.status.pGetDMG -= 0.3f;
                target.calcStatus();
            }
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

        if (target.status.element.IceRock == true)
        {
            target.status.element.IceRock = false;
            target.status.pIncreaseSpeed += 1.0f;
            target.calcStatus();
        }
        if (target.status.element.WindRock == true)
        {
            target.status.element.WindRock = false;
            target.status.pGetDMG -= 0.3f;
            target.calcStatus();
        }
        target.calcStatus();
        base.endEffect();
    }
}
