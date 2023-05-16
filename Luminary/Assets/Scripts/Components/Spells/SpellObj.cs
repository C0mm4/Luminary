using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObj : MonoBehaviour
{
    // Start is called before the first frame update

    public SpellData data;
    public float spawnTime, currentTime, deltaTime;
    public Vector3 spawnPos = new Vector3(0,0,0);

    public GameObject player;
    public GameObject target;

    public Vector3 mos;


    public virtual void Start()
    {
        player = GameManager.player;
    }

    public void setData(SpellData dts)
    {
        player = GameManager.player;
        data = dts;
        spawnTime = Time.time;
        currentTime = spawnTime;
        spawnPos = player.transform.position;
        

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION something");
        if (other.tag == "Mob")
        {
            other.GetComponent<Mob>().HPDecrease(data.damage);
            GameManager.Resource.Destroy(this.gameObject);
            Debug.Log("Mob Collision");
        }
        else if (other.tag == "Wall")
        {
            GameManager.Resource.Destroy(this.gameObject);
        }
    }

    public virtual void Update()
    {
        deltaTime = Time.time - currentTime;
        currentTime = Time.time;
    }
}
