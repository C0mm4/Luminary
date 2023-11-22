using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobProjectile : MobAttack
{
    public Vector3 dir;
    public bool isGravity;
    public bool isThrow;
    public Charactor player;

    // Start is called before the first frame update
    void Start()
    {
        isThrow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrow)
        {
            // Throw objects
            GetComponent<Rigidbody2D>().velocity = dir;
            transform.rotation = Quaternion.Euler(dir);
            if (isGravity)
            {
                dir.y -= Time.deltaTime * 10;
            }
        }
    }

    public override void setData(Mob mob)
    {
        // Set Target, attackers, transforms
        base.setData(mob);
        player = shooter.player;
        transform.position = shooter.transform.position;
        Debug.Log("Projectile Set");
    }

    public override void Activate()
    {
        isThrow = true;
    }
}
