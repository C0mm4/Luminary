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
    public int[][] roomGrid;
    public int mobCount;


    public int gateU, gateD, gateL, gateR;

    // set position
    public void set()
    {
        gateU = gateD = gateL = gateR = -1;
        this.gameObject.transform.position = new Vector3((float)(x * 20.5), y * 11, 0);
    }

    // Loading Objects in this Room
    void setObjects()
    {

    }
}
