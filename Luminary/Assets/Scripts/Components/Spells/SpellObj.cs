using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObj : MonoBehaviour
{
    // Start is called before the first frame update

    public SpellData data;
    public float spawnTime, currentTime;
    public Vector3 spawnPos = new Vector3(0,0,0);

    public GameObject player;
    public GameObject target;


    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void setData(SpellData dts)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        data = dts;
        spawnTime = Time.time;
        spawnPos.x = player.transform.position.x;
        

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "Mob")
        {
            obj.GetComponent<Mob>().HPDecrease(data.damage);
            GameManager.Resource.Destroy(this.gameObject);
        }
    }

}
