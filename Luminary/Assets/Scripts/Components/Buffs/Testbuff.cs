using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testbuff : Buff
{
    
    public Testbuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        this.durate = 5f;
    }
    
    public new void durateEffect()
    {
        target.speed += 1;

        base.durateEffect();
    }
}
