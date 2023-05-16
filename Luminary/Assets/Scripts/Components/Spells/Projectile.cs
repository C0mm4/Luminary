using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SpellObj
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
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void Update()
    {
        base.Update();
        transform.position += dir * 7 * deltaTime;
    }
}
