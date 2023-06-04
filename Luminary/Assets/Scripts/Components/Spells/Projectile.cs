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

        dir = mos - spawnPos;
        dir.z = 0;
        dir.Normalize();

        transform.position = player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        startPos = transform.position;
    }

    public override void Update()
    {
        base.Update();

        if (IsPointInEllipse())
        {
            transform.position += dir * speed * deltaTime;

        }
        else
        {
            GameManager.Resource.Destroy(this.gameObject);
        }
    }

    public bool IsPointInEllipse()
    {
        float result = ((transform.position.x - startPos.x) * (transform.position.x - startPos.x)) / (data.xRange * data.xRange)
               + ((transform.position.y - startPos.y) * (transform.position.y - startPos.y)) / (data.yRange * data.yRange);

        // 결과가 1 이하인 경우, 타원 내에 있는 것으로 판단
        return result <= 1.0f;
    }
}
