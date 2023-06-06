using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : SpellObj
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector3 dir;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        dir = target.transform.position - spawnPos;
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

        transform.position += dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        Debug.Log("Collision : " + other.gameObject.GetHashCode());
        if(other == target.gameObject)
        {
            for(int i = 0; i < data.hits; i++)
            {
                other.GetComponent<Mob>().HPDecrease(data.damage);
            }
            OnDestroy();

        }
    }
}
