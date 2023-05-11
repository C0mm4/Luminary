using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellFire : Fire
{
    [SerializeField]
    Vector3 mos;
    [SerializeField]
    Vector3 dir;

    public override void Start()
    {
        base.Start();
        mos = Input.mousePosition;

        dir = mos - spawnPos;
        dir.Normalize();

        transform.position = player.transform.position;

    }

    public void Update()
    {
        currentTime = Time.time;
        GetComponent<Rigidbody2D>().velocity = dir * 2;
    }
}