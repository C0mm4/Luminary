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
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(getState().GetType().Name);
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C");
            Buff newbuff = new Freeze(this, this, 1);
            
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(status.buffs.Count);
        }
    }

}
