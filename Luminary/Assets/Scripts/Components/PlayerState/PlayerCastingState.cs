using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingState : State
{
    Spell spell;
    GameObject target;
    float castingT;
    float startT;
    Vector3 mos = new Vector3();

    public PlayerCastingState(Spell spl, Vector3 mos) : base()
    {
        spell = spl;
        castingT = spl.data.castTime;
        this.mos = mos;
    }

    public PlayerCastingState(Spell spl, GameObject obj) : base()
    {
        spell = spl;
        castingT = spl.data.castTime;
        target = obj;
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
            switch (spell.data.type)
            {
                case 0:
                case 1:
                    spell.execute(mos);
                    break;
                case 2:
                    spell.execute(target);
                    break;
                case 3:
                    spell.execute(mos);
                    break;
            }
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
