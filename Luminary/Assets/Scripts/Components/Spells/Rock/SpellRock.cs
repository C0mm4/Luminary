using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellRock : Projectile
{
   
    public override void Start()
    {
        base.Start();
        
    }

    public override void Update()
    {
        base.Update();
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mob")
        {
            if (GameManager.Random.getGeneralNext(0, 100) <= data.debufP * 100)
            {
                Buff newbuff = new RockBuff(other.gameObject.GetComponent<Charactor>(), player.GetComponent<Charactor>());
            }
            other.GetComponent<Charactor>().changeState(new MobHitState(other.transform.position - this.transform.position));
        }

        base.OnTriggerEnter2D(other);
    }

}