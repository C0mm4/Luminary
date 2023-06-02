using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SpellObj
{

    [SerializeField]
    float speed;

    [SerializeField]
    Vector3 dir;

    public override void Start()
    {
        base.Start();

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
        transform.position += dir * speed * deltaTime;
    }
}
