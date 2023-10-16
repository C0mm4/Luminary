using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobProjectile : MobAttack
{
    public Vector3 dir;
    public bool isGravity;
    public bool isThrow;
    public Mob shooter;
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
            GetComponent<Rigidbody2D>().velocity = dir;
            transform.rotation = Quaternion.Euler(dir);
            if (isGravity)
            {
                dir.y -= Time.deltaTime * 10;
            }
        }
    }

    public virtual void setData(Mob mob)
    {
        shooter = mob;
        player = shooter.player;
        transform.position = shooter.transform.position;
        
    }

    public virtual void Throw()
    {
        isThrow = true;
        shooter.AtkObj = null;
    }
}
