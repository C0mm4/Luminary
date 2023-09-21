using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
public class Room : MonoBehaviour
{
    
    public int x, y;
    [SerializeField]
    public int index;
    public int mobCount = 0;


    public bool gateU = false;
    public bool gateD = false;
    public bool gateL = false;
    public bool gateR = false;

    [SerializeField]
    public List<Vector3Int> doorPos = new List<Vector3Int>();

    [SerializeField]
    public List<GameObject> enemyPos = new List<GameObject>();

    [SerializeField]
    public Tilemap wallTileMap;

    [SerializeField]
    public SpriteRenderer bg;

    [SerializeField]
    public List<GameObject> objs;

    [SerializeField]
    public GameObject components;
    [SerializeField]
    public GameObject enemies;

    [SerializeField]
    public int types;


    // set position
    public void set()
    {
        this.gameObject.transform.position = new Vector3((float)(x), (y), 2);

        foreach(GameObject t in enemyPos)
        {
            GameObject go = GameManager.Resource.Instantiate("Mobs/TestMob", enemies.transform);
            Debug.Log(go);
            go.transform.position = t.transform.position;
            go.transform.Rotate(90f, 0f, 0f);
            mobCount++;
            objs.Add(go);
            go.SetActive(false);
        }
        if(mobCount == 0)
        {
            clearRoom();
        }
    }

    public void ActiveEnemies()
    {
        Invoke("ActiveEnemiesCallback", 0.5f);
    }


    private void ActiveEnemiesCallback()
    {
        foreach (GameObject obj in objs)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    public void clearRoom()
    {
        GameManager.StageC.ClearRoom();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            Debug.Log("ASD");
            if(GameManager.StageC.currentRoom != index)
            {
                GameManager.StageC.moveRoom(index);
            }
        }
    }
}
