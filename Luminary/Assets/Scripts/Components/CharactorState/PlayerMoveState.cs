using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : State
{
    Vector3 targetPos = new Vector3();
    Vector3 dir = new Vector3();
    float beforeTime;
    float time;
    int cnt = 0;
    public override void EnterState(Charactor chr)
    {
        time = Time.time;
        beforeTime = Time.time;
        targetPos = GameManager.inputManager.mouseWorldPos;
        dir = new Vector3(targetPos.x - chr.transform.position.x, targetPos.y - chr.transform.position.y, 0);
        dir.Normalize();

        /*
        GameManager.inputManager.KeyAction -= chr.GetComponent<Player>().onKeyboard;*/
        base.EnterState(chr);
    }
    public override void UpdateState()
    {
        if(Time.time - time > 1f)
        {
            time = Time.time;
            Debug.Log(cnt);
            cnt = 0;
        }
        beforeTime = Time.time - beforeTime;
        dir = new Vector3(targetPos.x - charactor.transform.position.x, targetPos.y - charactor.transform.position.y, 0);
        dir.Normalize();
        charactor.GetComponent<Rigidbody2D>().velocity = dir * (charactor.status.speed);

        base.UpdateState();
        if (Vector3.Distance(targetPos, charactor.transform.position) <= 1.1f)
        {
            Debug.Log("MoveFinish");
            charactor.GetComponent<Charactor>().changeState(new PlayerIdleState());
        }

        beforeTime = Time.time;
        cnt += 1;
    }
    public override void ExitState()
    {
        base.ExitState();

    }
}
