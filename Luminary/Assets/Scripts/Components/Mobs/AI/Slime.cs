using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AIModel
{
    public override void FixedUpdate()
    {
        if (GameManager.player != null)
        {
            if (target.playerDistance().magnitude <= target.data.detectDistance)
            {
                if(target.getState().GetType().Name != "MobStunState")
                {
                    target.changeState(new MobChaseState());
                }
            }
            else
            {
                if (target.getState().GetType().Name == "MobChaseState")
                {
                    target.endCurrentState();
                }
                else if (target.getState().GetType().Name != "MobStunState")
                {
                    target.changeState(new MobIdleState());
                }
            }
        }
    }
}
