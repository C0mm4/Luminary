using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[DisallowMultipleComponent]
public class Room : MonoBehaviour
{
    
    public int x, y;
    public int width, height;
    public int index;
    public int[,] roomGrid;
    public int mobCount;


    public int gateU, gateD, gateL, gateR;
    public string gatedir = "0000";

    // set position
    public void set()
    {
        gateU = gateD = gateL = gateR = -1;
        this.gameObject.transform.position = new Vector3((float)(x * 19.2f), (y * 10.8f), 0);
        roomGrid = new int[19,33];
    }

    public void setData(int[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log(data[i]);
            int si = i % 33;
            int sj = i / 33;
            this.roomGrid[sj, si] = data[i];
        }
    }

    // Loading Objects in this Room
    public void setObjects()
    {
        
        for (int i = 0; i < roomGrid.GetLength(0); i++)
        {
            for (int j = 0;  j < roomGrid.GetLength(1); j++)
            {
                Debug.Log(roomGrid[i, j]);
                if (roomGrid[i,j] != 0)
                {
                    GameObject go = new GameObject();
                    switch(roomGrid[i,j])
                    {
                        case 1:
                            GameManager.Resource.Destroy(go);
                            go = GameManager.Resource.Instantiate("Dungeon/Wall");
                            break;
                        case 2:
                            GameManager.Resource.Destroy(go);
                            go = GameManager.Resource.Instantiate("Dungeon/Rock");
                            break;
                    }
                    go.transform.parent = this.transform;
                    go.transform.position = new Vector3((this.transform.position.x + ((j-16) * 0.52f)) , (this.transform.position.y + (-(i-9) * 0.52f)), 0);
                }
            }
        }
    }
}
