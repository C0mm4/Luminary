using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellStoneHand : Field
{
    Vector3 dir;

    float TickTime = 1f;
    float lastTickTime;
    bool onTick;

    List<GameObject> trig;

    public override void Start()
    {
        base.Start();
        lastTickTime = Time.time - TickTime;
        onTick = true;
        trig = new List<GameObject>();
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
        }

        base.OnTriggerEnter2D(other);
    }

}