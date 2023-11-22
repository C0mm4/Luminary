using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Mob Attack Objects
public class GoblinArrow : MobProjectile
{
    public override void setData(Mob mob)
    {
        base.setData(mob);
        if(shooter.sawDir.x < 0)
        {
            transform.position += new Vector3(-0.1f, 0, 0);
        }
        else
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
    }
    public override void Activate()
    {
        base.Activate();
        dir = new Vector3(player.transform.position.x - shooter.transform.position.x, 4, 0);
    }
}
