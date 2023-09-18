using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunRoom : MonoBehaviour
{
    public int roomID;
    public int x, y;
    public int centerX, centerY;
    public GameObject Tiles;
    public List<Tile> tiles = new List<Tile>();
    public List<GameObject> doorTiles = new List<GameObject>();
    public Tile leftDoor;
    public Tile rightDoor;
    public Tile upperDoor;
    public Tile downDoor;

    public int sizeX, sizeY;

    public GameObject enterTrigger;

    public bool isClear = false;
    public bool isActivate = false;

    public List<Transform> spawnTrans = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeDoor(int x, int y)
    {
        Tile tile = new Tile();
        for(int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].x == x && tiles[i].y == y)
            {
                tile = tiles[i];
                break;
            }
        }

        switch (tile.types)
        {
            case 1:
                break; 
            case 2:
                GameObject go = GameManager.Resource.Instantiate(doorTiles[0], transform);
                go.transform.position = tile.transform.position;
                GameManager.Resource.Destroy(tile.gameObject);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;

        }
    }
}
