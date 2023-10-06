using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AIModel
{
    public override void Update()
    {
        if(GameManager.player != null)
        {
            string state = target.getState().GetType().Name;
            if(state != "MobStunState" && state != "MobCastState" && state != "MobATKState")
            {
                if(target.playerDistance().magnitude <= target.data.detectDistance)
                {

                    if (target.playerDistance().magnitude <= target.data.attackRange)
                    {
                        target.changeState(new MobCastState(target.data.castSpeed, 0));
                    }
                    else
                    {
                        target.changeState(new MobChaseState());
                    }
                }
            }
        }
    }


}