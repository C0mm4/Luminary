using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAbsolState : State
{
    Vector3 targetPos = new Vector3();
    Vector3 dir = new Vector3();
    public PlayerMoveAbsolState(Vector2 pos)
    {
        targetPos = pos;

    }

    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        dir = new Vector3(targetPos.x - chr.transform.position.x, targetPos.y - chr.transform.position.y, 1);
        dir.Normalize();

        Debug.Log("AbsolMove target pos : " + targetPos);
    }

    public override void ExitState()
    {

        charactor = null;
    }

    public override void ReSetState()
    {
        if (Vector3.Dot(targetPos - charactor.transform.position, dir) <= 0)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }
        else
        {

        }
    }

    public override void UpdateState()
    {
        if (Vector3.Dot(targetPos - charactor.transform.position, dir) <= 0)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }
        else
        {
            dir = new Vector3(targetPos.x - charactor.transform.position.x, targetPos.y - charactor.transform.position.y, 1);
            dir.Normalize();
            charactor.GetComponent<Rigidbody2D>().velocity = dir * (charactor.status.speed);
        }
    }
}
