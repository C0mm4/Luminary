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
        this.gameObject.transform.position = new Vector3((float)(posx * 19.2), posy * 10.8f, 3);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            int croom = GameManager.StageC.currentRoom;
            Debug.Log("PlayerCollision");
            GameManager.StageC.moveRoom(croom != room1 ? room1 : room2);
        }
    }
}
