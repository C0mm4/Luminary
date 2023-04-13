using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    public int room1, room2;
    public float posx, posy;
    public int index;

    // set position
    public void set()
    {
        this.gameObject.transform.position = new Vector3((float)(posx * 20.5), posy * 11, 0);
    }
}
