using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbuff : Buff
{
    
    public Testbuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(5);
        target.status.pIncreaseSpeed += 0.1f;
        target.calcStatus();
        Debug.Log(durate);
    }
    
    public override void durateEffect()
    {

        base.durateEffect();
    }

    public override void resetEffect(int i)
    {
        target.status.pIncreaseSpeed -= 0.1f;
        target.calcStatus();
        base.resetEffect(i);
    }

    public override void endEffect()
    {
        target.status.pIncreaseSpeed -= 0.1f;
        target.calcStatus();
        base.endEffect();
    }
}
