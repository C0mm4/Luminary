using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCastState : State
{
    float castTime;
    float castStartT;
    int index;

    public MobCastState(float t, int index)
    {
        castTime = t;
        castStartT = Time.time;
        this.index = index;
        
    }

    public override void EnterState(Charactor chr)
    {
        Debug.Log("Enter Cast");
        charactor = chr;
        charactor.charactorSpeed = new Vector2 (0, 0);
        charactor.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
    }

    public override void ExitState()
    {
        charactor = null;
    }

    public override void ReSetState()
    {
        charactor.endCurrentState();
    }

    public override void UpdateState()
    {
        charactor.AnimationPlay("CastAnimation " + index);
        float currentT = Time.time;
        if(currentT - castStartT >= castTime)
        {
            
            charactor.changeState(new MobATKState(index));
        }
    }
}
