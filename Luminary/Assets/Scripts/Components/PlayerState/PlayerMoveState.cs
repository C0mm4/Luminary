using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : State
{
    
    Vector2 dir = new Vector2();
    int cnt = 0;
    public override void EnterState(Charactor chr)
    {
        charactor = chr;

    }
    public override void UpdateState()
    {
        
        if (charactor.GetComponent<Player>().ismove)
        {
            if(charactor.GetComponent<Player>().playerSpeed == Vector2.zero) 
            {
                charactor.GetComponent<Charactor>().endCurrentState();
            }
            else
            {
                dir = charactor.GetComponent<Player>().playerSpeed.normalized * charactor.GetComponent<Player>().playerSpeed;
                charactor.GetComponent<Rigidbody2D>().velocity = charactor.GetComponent<Player>().playerSpeed;
            }

            
        }


    }
    public override void ReSetState()
    {

    }

    public override void ExitState()
    {
        charactor.GetComponent<Player> ().ismove = false;
        charactor = null;
    }
}
