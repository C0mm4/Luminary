using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : Buff
{
    public FireBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        this.durate = 5;
        tickTime = 1;
        Debug.Log("FIREDEBUF0");
        
    }

    public override void onTick()
    {
        target.HPDecrease(1);
        Debug.Log("On Tick");
        base.onTick();
    }
}
