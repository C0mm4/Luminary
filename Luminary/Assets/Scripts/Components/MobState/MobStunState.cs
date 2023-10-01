using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStunState : State
{
    public float time;
    public float startTime;
    public float currentTime;
    public MobStunState()
    {
        time = 0;
        startTime = Time.time;
    }

    public MobStunState(float time)
    {
        this.time = time;
        startTime = Time.time;
    }

    public override void EnterState(Charactor chr)
    {
        this.charactor = chr;
        Debug.Log(charactor);
        charactor.canMove = false;
    }

    public override void ExitState()
    {
        this.charactor.canMove = true;
        Debug.Log("Exit");
        this.charactor = null;
    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }

    public override void UpdateState()
    {
        if (time != 0)
        {
            if(Time.time - startTime >= time)
            {
                Debug.Log(charactor);
                charactor.endCurrentState();
            }
        }
    }


}
