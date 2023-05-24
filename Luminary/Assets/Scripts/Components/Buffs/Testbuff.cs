using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbuff : Buff
{
    
    public Testbuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(5);
        target.speedIncrease += 10;

        Debug.Log(durate);
    }
    
    public override void durateEffect()
    {

        base.durateEffect();
    }

    public override void resetEffect(int i)
    {
        target.speedIncrease -= 10;
        base.resetEffect(i);
    }

    public override void endEffect()
    {
        target.speedIncrease -= 10;
        base.endEffect();
    }
}
