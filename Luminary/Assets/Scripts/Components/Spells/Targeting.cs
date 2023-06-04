using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : SpellObj
{
    [SerializeField]
    float speed;

    [SerializeField]
    Vector3 dir;
    // Start is called before the first frame update
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

    // Update is called once per frame
    public override void Update()
    {
        dir = target.transform.position - transform.position;
        dir.z = 0;
        dir.Normalize();

        transform.position = player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other == target)
        {

            base.OnTriggerEnter2D(other);

        }
    }
}
