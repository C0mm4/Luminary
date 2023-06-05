using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : SpellObj
{
    [SerializeField]
    float speed;

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

        if (Time.time - spawnTime > data.durateT)
        {
            GameManager.Resource.Destroy(gameObject);
        }

        if (!onTick)
        {
            if(Time.time - lastTickTime >= TickTime)
            {
                onTick = true;
            }
        }
        else
        {
            foreach(GameObject obj in trig)
            {
                obj.GetComponent<Charactor>().HPDecrease(data.damage);
            }
        }
    }
    


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mob")
        {
            if (onTick)
            {
                trig.Add(other.gameObject);
            }
        }
    }

}