using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : State
{
    // Start is called before the first frame update

    Vector3 dir = new Vector3();
    public override void EnterState(Charactor chr)
    {
        charactor = chr;

        Vector3 targetPos = GameManager.inputManager.mouseWorldPos;
        targetPos.z = 1;

        dir = new Vector3(targetPos.x - chr.transform.position.x, targetPos.y - chr.transform.position.y, 1);
        dir.Normalize();
        charactor.GetComponent<Charactor>().status.increaseSpeed += 5;
        charactor.GetComponent<Charactor>().calcStatus();

        charactor.GetComponent<Rigidbody2D>().velocity = dir * (charactor.status.speed);

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
        charactor.GetComponent<Charactor>().status.increaseSpeed -= 5;
        charactor.GetComponent<Charactor>().calcStatus();
        charactor.GetComponent<Rigidbody2D>().velocity = dir * (charactor.status.speed);
        charactor = null;
        
    }
}
