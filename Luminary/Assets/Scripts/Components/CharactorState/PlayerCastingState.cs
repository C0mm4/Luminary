using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingState : State
{
    Spell spell;
    float castingT;
    float startT;
    public PlayerCastingState(Spell spl) : base()
    {
        spell = spl;
        castingT = spl.data.castTime;
    }

    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        startT = Time.time;
        charactor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Debug.Log(castingT);
    }

    public override void UpdateState()
    {
        if(Time.time - startT >= castingT)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
            spell.execute();
        }
    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }

    public override void ExitState()
    {

    }
}
