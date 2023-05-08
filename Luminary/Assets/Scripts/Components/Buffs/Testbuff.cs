using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbuff : Buff
{
    
    public Testbuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        this.durate = 5f;
        target.speedIncrease += 10;
    }
    
    public override void durateEffect()
    {

        Debug.Log("spdUp");
        base.durateEffect();
    }

    public override void endEffect()
    {
        target.speedIncrease -= 10;
        base.endEffect();
    }
}
