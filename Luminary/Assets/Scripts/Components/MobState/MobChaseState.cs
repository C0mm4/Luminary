using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MobChaseState : State
{

    public override void EnterState(Charactor chr)
    {
        charactor = chr;
    }


    public override void UpdateState()
    {
        charactor.AnimationPlay("MoveAnimation");
        if (GameManager.player == null)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }
        else
        {
            Vector3 dir = new Vector3(charactor.player.transform.position.x - charactor.transform.position.x,
                           charactor.player.transform.position.y - charactor.transform.position.y,
                           charactor.player.transform.position.z - charactor.transform.position.z);
            dir.Normalize();
            charactor.charactorSpeed = dir;
            charactor.GetComponent<Rigidbody2D>().velocity = dir * charactor.status.speed;
            charactor.GetComponent<Charactor>().sawDir.x = Vector2.Dot(charactor.GetComponent<Charactor>().charactorSpeed, new Vector2(1, 0));
        }

    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }

    public override void ExitState()
    {
        charactor = null;
    }
}
