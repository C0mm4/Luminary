using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : State
{
    // Start is called before the first frame update

    Vector2 dir = Vector2.one;
    Vector2 pdir = new Vector2();
    public override void EnterState(Charactor chr)
    {
        charactor = chr;


        Debug.Log(charactor.GetComponent<Player>().playerSpeed.x);
        dir = dir * charactor.GetComponent<Player>().playerSpeed;
        dir = dir.normalized;
        pdir = charactor.GetComponent<Player>().playerSpeed;
        pdir = pdir.normalized * pdir;
        charactor.GetComponent<Rigidbody2D>().velocity = charactor.GetComponent<Player>().playerSpeed + dir * 5;

    }
    
    public override void UpdateState()
    {
    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }

    public override void ExitState()
    {
        charactor.GetComponent<Rigidbody2D>().velocity = charactor.GetComponent<Player>().playerSpeed;
        charactor = null;
        
    }
}
