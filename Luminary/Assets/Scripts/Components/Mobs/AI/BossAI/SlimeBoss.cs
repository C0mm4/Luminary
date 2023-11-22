using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : AIModel
{
    private bool[] patturns = new bool[3];
    private bool isMove = false;
    private float moveTime;

    public override void FixedUpdate()
    {
        if(GameManager.player != null)
        {
            // if is move, wait for seconds, to next action
            if (isMove)
            {
                if(Time.time - moveTime >= 1f)
                {
                    isMove = false;
                    target.setIdleState();
                    target.EffectGen("Effect/SlimeBoss/MoveEnd");
                }
            }
            else
            {
                if(target.HPPercent() <= 0.5 && !patturns[0])
                {
                    patturns[0] = true;
                    target.changeState(new MobCastState(2f, 0));
                }

                else if(Time.time - moveTime >= 1.3f)
                {
                    moveTime = Time.time;
                    isMove = true;
                    target.changeState(new MobChaseState());
                }
            }
        }
    }

}
