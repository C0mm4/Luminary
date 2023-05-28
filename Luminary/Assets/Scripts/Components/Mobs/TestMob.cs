using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : Mob
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();

            if(player == null)
            {
                sMachine.changeState(new MobIdleState(), this);
            }
        }
        else
        {
            if (sMachine.getState().GetType() != typeof(MobChaseState))
            {
                sMachine.changeState(new MobChaseState(), this);
            }
        }

        base.Update();
    }

}
