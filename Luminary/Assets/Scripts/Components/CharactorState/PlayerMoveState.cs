using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : State
{
    Vector3 targetPos = new Vector3();
    Vector3 dir = new Vector3();
    int cnt = 0;
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        targetPos = GameManager.inputManager.mouseWorldPos;
        targetPos.z = 1;
        dir = new Vector3(targetPos.x - chr.transform.position.x, targetPos.y - chr.transform.position.y, 1);
        dir.Normalize();

        if (Vector3.Distance(charactor.transform.position, targetPos) <= 0.2f)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }

        /*
        GameManager.inputManager.KeyAction -= chr.GetComponent<Player>().onKeyboard;*/
    }
    public override void UpdateState()
    {
        

        if (Vector3.Dot(targetPos - charactor.transform.position, dir) <= 0)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }
        else
        {
            dir = new Vector3(targetPos.x - charactor.transform.position.x, targetPos.y - charactor.transform.position.y, 0);
            dir.Normalize();
            charactor.GetComponent<Rigidbody2D>().velocity = dir * (charactor.status.speed);
        }
    }
    public override void ReSetState()
    {
        if(Vector3.Dot(targetPos - charactor.transform.position, dir) <= 0)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }
        else
        {
        }
    }

    public override void ExitState()
    {

        charactor = null;
    }
}
