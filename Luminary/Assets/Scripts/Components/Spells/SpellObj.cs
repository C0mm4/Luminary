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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mob")
        {
            other.GetComponent<Mob>().HPDecrease(data.damage);
            OnDestroy();
        }
        else if (other.tag == "Wall")
        {
            OnDestroy();
        }
    }

    public virtual void Update()
    {
        deltaTime = Time.time - currentTime;
        currentTime = Time.time;
    }

    public virtual void OnDestroy()
    {
        GameManager.Resource.Destroy(this.gameObject);
    }
}
