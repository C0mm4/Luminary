using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHitState : State
{
    Vector3 dir = new Vector3();
    float startTime;


    public MobHitState(Vector3 dir)
    {
        this.dir = dir;
    }

    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        startTime = Time.time;
        charactor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        charactor.GetComponent<Charactor>().isHit = true;
    }

    public override void UpdateState()
    {
        if (Time.time - startTime >= 0.1f)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }
        else
        {
            charactor.GetComponent<Rigidbody>().velocity = dir * 1;
        }
    }

    public override void ExitState()
    {
        charactor.GetComponent<Charactor>().isHit = false;
        charactor = null;

    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }
}
