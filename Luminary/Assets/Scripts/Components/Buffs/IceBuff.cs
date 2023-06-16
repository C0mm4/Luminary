using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IceBuff : Buff
{
    public IceBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        target.status.pGetDMG += 0.03f;
        target.calcStatus();
        setDurate(3.0f);

        if (target.status.element.Wind == true)
        {
            target.status.element.IceWind = true;
        }
    }

    public override void durateEffect()
    {
        if(target != null)
        {
            target.status.element.Ice = true;
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
        target.status.element.Ice = false;
        target.status.pGetDMG -= 0.03f;
        target.calcStatus();

        if (target.status.element.IceWind == true)
        {
            target.status.element.IceWind = true;
        }
        base.endEffect();
    }
}
