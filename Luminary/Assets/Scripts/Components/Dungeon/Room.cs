using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[DisallowMultipleComponent]
public class Room : MonoBehaviour
{
    // Start is called before the first frame update

    public int x, y;
    public int width, height;
    public int index;
    public int[][] roomGrid;
    public int mobCount;


    public int gateU, gateD, gateL, gateR;

    public void set()
    {
        gateU = gateD = gateL = gateR = -1;
        this.gameObject.transform.position = new Vector3(x * 11, y * 11, 0);
    }
    void setObjects()
    {

    }
}
