using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    public int room1, room2;
    public float posx, posy;
    public int index;

    public void set()
    {
        this.gameObject.transform.position = new Vector3(posx * 11, posy * 11, 0);
    }
}
