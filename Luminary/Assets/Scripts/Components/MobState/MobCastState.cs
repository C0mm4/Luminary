using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCastState : State
{
    float castTime;
    float castStartT;
    int index;

    // set casting time, attack prefab index
    public MobCastState(float t, int index)
    {
        castTime = t;
        castStartT = Time.time;
        this.index = index;
        
    }

    // can't move when casting
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        charactor.charactorSpeed = new Vector2 (0, 0);
        charactor.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
    }

    public override void ExitState()
    {
        charactor = null;
    }

    public override void ReSetState(Charactor chr)
    {
        charactor.endCurrentState();
    }

    // matching animation speed and cast speed, animation play
    public override void UpdateState()
    {
        charactor.AnimationPlay("CastAnimation " + index, 1 / castTime);
        float currentT = Time.time;
        // When casting time is enough, attack start
        if(currentT - castStartT >= castTime)
        {
            charactor.changeState(new MobATKState(index));
        }
    }
}
