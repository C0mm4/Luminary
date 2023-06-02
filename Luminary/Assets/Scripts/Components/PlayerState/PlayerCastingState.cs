using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingState : State
{
    Spell spell;
    float castingT;
    float startT;
    Vector3 mos = new Vector3();

    public PlayerCastingState(Spell spl, Vector3 mos) : base()
    {
        spell = spl;
        castingT = spl.data.castTime;
        this.mos = mos;
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
            spell.execute(mos);
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
