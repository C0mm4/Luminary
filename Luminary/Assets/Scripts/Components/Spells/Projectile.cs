using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Projectile : SpellObj
{

    [SerializeField]
    float speed;

    [SerializeField]
    Vector3 dir;

    Vector3 startPos = Vector3.zero;

    public override void Start()
    {
        base.Start();
        // Set Projectile Directions
        dir = mos - spawnPos;
        dir.z = 0;
        dir.Normalize();
        // Set Projectile spell transforms
        transform.position = player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        startPos = transform.position;
    }

    public override void Update()
    {
        // Set Projectile transfrom on frame
        base.Update();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // if in ellipse process projectile
        if (IsPointInEllipse())
        {
            transform.position += dir * speed * deltaTime;

        }
        // if object is out of ellipse destoy object.
        else
        {
            GameManager.Resource.Destroy(this.gameObject);
        }
    }

    public bool IsPointInEllipse()
    {
        float result = ((transform.position.x - startPos.x) * (transform.position.x - startPos.x)) / (data.xRange * data.xRange)
               + ((transform.position.y - startPos.y) * (transform.position.y - startPos.y)) / (data.yRange * data.yRange);

        // if return <= 1, this object in ellipse
        return result <= 1.0f;
    }
}
