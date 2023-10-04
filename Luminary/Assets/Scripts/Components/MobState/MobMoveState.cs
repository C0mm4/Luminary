using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMoveState : State
{
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
    }

    public override void ExitState()
    {
        charactor = null;
    }

    public override void ReSetState()
    {
        EnterState(charactor);
    }

    public override void UpdateState()
    {
        charactor.AnimationPlay("MoveAnimation");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
