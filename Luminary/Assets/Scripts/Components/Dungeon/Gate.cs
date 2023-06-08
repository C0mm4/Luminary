using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
            GameManager.Instance.moveRoom(croom != room1 ? room1 : room2);
            other.GetComponent<Charactor>().endCurrentState();
            switch(GetPointPosition(other.transform.position, transform.position))
            {
                case PointPosition.Left:
                    other.GetComponent<Charactor>().changeState(new PlayerMoveAbsolState(transform.position + new Vector3(-3.5f, 0f, 0f)));
                    break;
                case PointPosition.Right:
                    other.GetComponent<Charactor>().changeState(new PlayerMoveAbsolState(transform.position + new Vector3(3.5f, 0f, 0f)));
                    break;
                case PointPosition.Up:
                    other.GetComponent<Charactor>().changeState(new PlayerMoveAbsolState(transform.position + new Vector3(0f, 2.7f, 0f)));
                    break;
                case PointPosition.Down:
                    other.GetComponent<Charactor>().changeState(new PlayerMoveAbsolState(transform.position + new Vector3(0f, -2.7f, 0f)));
                    break;
            }
        }
    }

    public PointPosition GetPointPosition(Vector2 playerP, Vector2 gateP)
    {
        float horizonDist = gateP.x - playerP.x;
        float verticalDist = gateP.y - playerP.y;
        if(Mathf.Abs(horizonDist) > Mathf.Abs(verticalDist))
        {
            if(horizonDist > 0)
            {
                Debug.Log("Right");
                return PointPosition.Right;
            }
            else
            {
                return PointPosition.Left;
            }

        }
        else
        {
            if(verticalDist > 0)
            {
                return PointPosition.Up;
            }
            else
            {
                return PointPosition.Down;
            }
        }
    }
}
