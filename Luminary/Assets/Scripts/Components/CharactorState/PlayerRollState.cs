using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : State
{
    // Start is called before the first frame update
    float startrolltime;
    public override void EnterState(Charactor chr)
    {
        base.EnterState(chr);

        startrolltime = Time.time;

    }


}
