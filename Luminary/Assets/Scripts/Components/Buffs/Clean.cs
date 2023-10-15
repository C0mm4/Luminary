using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Clean : Buff
{
    public Clean(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 9;
        setDurate(999f);
//        setDurate(1f);
        setTickTime(0);

        cooltime = 10f;

        this.dmg = 1;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.clean)
        {
            base.startEffect();
            target.HPDecrease(dmg);
            target.status.element.clean = true;
            target.buffCool(cooltime, id);
            target.changeState(new MobStunState(1f));
        }
    }

    public override void endEffect()
    {
        target.endCurrentState();
        base.endEffect();
    }

    public override bool checkCombinate()
    {
        return false;
    }


}
