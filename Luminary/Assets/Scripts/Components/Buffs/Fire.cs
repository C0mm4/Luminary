using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Buff
{

    public Fire(Charactor tar, Charactor atk, int dmg) : base(tar, atk) 
    {
        id = 14;
        setDurate(0f);
        setTickTime(0f);

        cooltime = 10f;

        dmg = 3 * dmg;
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.fire)
        {
            base.startEffect();
            target.status.element.fire = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        GameObject go = GameManager.Resource.Instantiate("Spell/Field/Buff/FireField");
        go.GetComponent<Field>().dmg = dmg;
        base.endEffect();
    }
}
