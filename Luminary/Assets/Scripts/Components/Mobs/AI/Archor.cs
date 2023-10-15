using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Archor : AIModel
{
    public override void Update()
    {
        
        if(GameManager.player != null)
        {
            string state = target.getState().GetType().Name;
            
            if (state != "MobStunState" &&  state != "MobCastState" && state != "MobATKState")
            {
                if(target.playerDistance().magnitude <= target.data.detectDistance)
                {
                    Debug.Log(target.playerDistance().magnitude <= target.data.runRange);
                    if(target.playerDistance().magnitude <= target.data.runRange)
                    {
                        target.changeState(new MobRunState());
                    }
                    else
                    {
                        if(state == "MobRunState")
                        {
                            if(target.playerDistance().magnitude >= target.data.runDistance)
                            {
                                target.setIdleState();
                            }
                        }
                        else
                        {
                            if(target.playerDistance().magnitude <= target.data.attackRange)
                            {
                                if(Vector2.Dot(target.playerDir(), target.sawDir) > 0)
                                {
                                    target.changeState(new MobCastState(target.data.castSpeed, 0));
                                }
                                else
                                {
                                    target.changeState(new MobChaseState());
                                }
                            }
                            else
                            {
                                target.changeState(new MobChaseState());
                            }
                        }
                    }
                }
                else
                {
                    target.setIdleState();
                }

            }
        }
    }

}
