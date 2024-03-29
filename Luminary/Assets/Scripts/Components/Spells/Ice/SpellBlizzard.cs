using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellBlizzard : Field
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
                Buff newbuff = new IceBuff(other.gameObject.GetComponent<Charactor>(), player.GetComponent<Charactor>());
            }
        }

        base.OnTriggerEnter2D(other);
    }

}