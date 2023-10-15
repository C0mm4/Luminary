using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellMagicMissile : Projectile
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

//          Buff newbuff = new IceBuff(other.gameObject.GetComponent<Charactor>(), player.GetComponent<Charactor>());

            other.GetComponent<Charactor>().changeState(new MobHitState(other.transform.position - this.transform.position));
        }

        base.OnTriggerEnter2D(other);
    }

}