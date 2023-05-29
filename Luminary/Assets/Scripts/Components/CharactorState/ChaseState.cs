using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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
        Vector3 dir = new Vector3(charactor.player.transform.position.x - charactor.transform.position.x,
                                   charactor.player.transform.position.y - charactor.transform.position.y,
                                   charactor.player.transform.position.z - charactor.transform.position.z);

        dir.Normalize();
        charactor.GetComponent<Rigidbody2D>().velocity = dir * charactor.status.speed;
    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }

    public override void ExitState()
    {
        charactor = null;
    }
}
