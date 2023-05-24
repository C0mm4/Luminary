using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : Buff
{
    public FireBuff(Charactor tar, Charactor atk) : base(tar, atk)
    {
        setDurate(5);
        setTickTime(1);
        Debug.Log(Time.time);
        
    }

    public override void durateEffect()
    {
        if (currentTime - lastTickTime >= tickTime)
        {
            Debug.Log(currentTime);
            Debug.Log(lastTickTime);
            onTick();
            lastTickTime = currentTime;
        }
        base.durateEffect();
    }

    public override void onTick()
    {
        target.HPDecrease(1);
        Debug.Log(Time.time);
        Debug.Log("On Tick");
        base.onTick();
    }
}
