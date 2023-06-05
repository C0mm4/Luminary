using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellWindCutter : Targeting
{
    Vector3 dir;


    public override void Start()
    {
        base.Start();
        mos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        dir = mos - spawnPos;
        dir.z = 0;
        dir.Normalize();

        transform.position = player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
                Buff newbuff = new WindBuff(other.gameObject.GetComponent<Charactor>(), player.GetComponent<Charactor>());
            }
        }

        base.OnTriggerEnter2D(other);
    }

}