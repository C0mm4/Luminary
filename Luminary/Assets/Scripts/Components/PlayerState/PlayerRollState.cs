using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : State
{
    // Start is called before the first frame update

    Vector2 dir = Vector2.one;
    Vector2 pdir = new Vector2();

    Vector2 spellDir = new Vector2();
    public PlayerRollState()
    {

    }

    public PlayerRollState(Vector2 dir)
    {
        spellDir = dir;
    }

    public override void EnterState(Charactor chr)
    {
        charactor = chr;


        Debug.Log(spellDir.x);
        dir = dir * spellDir;
        dir = dir.normalized;
        pdir = spellDir;
        pdir = pdir.normalized * pdir;
        charactor.GetComponent<Rigidbody2D>().velocity = spellDir + dir * 5;

    }
    
    public override void UpdateState()
    {
    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }

    public override void ExitState()
    {
        charactor.GetComponent<Rigidbody2D>().velocity = charactor.GetComponent<Player>().charactorSpeed;
        charactor = null;
        
    }
}
