using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Archor : AIModel
{
    public override void Update()
    {
        // player exists
        if(GameManager.player != null)
        {
            string state = target.getState().GetType().Name;
            // target can move
            if (state != "MobStunState" &&  state != "MobCastState" && state != "MobATKState")
            {
                // player is in detect distance
                if(target.playerDistance().magnitude <= target.data.detectDistance)
                {
                    // if too close
                    if(target.playerDistance().magnitude <= target.data.runRange)
                    {
                        // run
                        target.changeState(new MobRunState());
                    }
                    else
                    {
                        // if start run, run away some distance
                        if(state == "MobRunState")
                        {
                            if(target.playerDistance().magnitude >= target.data.runDistance)
                            {
                                target.setIdleState();
                            }
                        }
                        else
                        {
                            // if player in attack range, and view direction is current, Attacked player
                            if(target.playerDistance().magnitude <= target.data.attackRange)
                            {
                                if(Vector2.Dot(target.playerDir(), target.sawDir) > 0)
                                {
                                    if(Time.time - target.lastAttackT >= target.data.castCool)
                                    {
                                        target.changeState(new MobCastState(target.data.castSpeed, 0));
                                    }
                                    else
                                    {
                                        target.setIdleState();
                                    }
                                }
                                else
                                {
                                    target.changeState(new MobChaseState());
                                }
                            }
                            // if player out of attack range, chase player
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
