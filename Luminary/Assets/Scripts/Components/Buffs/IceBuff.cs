using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IceBuff : Buff
{
    public IceBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(3.0f);
    }

    public override void durateEffect()
    {
        if(target != null)
        {
            target.status.element.Ice = true;
            target.status.pGetDMG = 1.03f;
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
        target.status.element.Ice = false;
        target.status.pGetDMG = 1.0f;
        target.calcStatus();
        base.endEffect();
    }
}
