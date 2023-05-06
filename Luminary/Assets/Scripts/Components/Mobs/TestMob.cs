using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMob : Mob
{
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
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

        sMachine.updateState();
    }
}
