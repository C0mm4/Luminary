using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    public int room1, room2;
    public float posx, posy;
    public int index;
    [SerializeField]
    GameObject collider;
    // set position

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            int croom = GameManager.StageC.currentRoom;
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

    public void closeGate()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);
        collider.SetActive(true);
    }

    public void openGate()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.2f);
        collider.SetActive(false);
    }

    public PointPosition GetPointPosition(Vector2 playerP, Vector2 gateP)
    {
        float horizonDist = gateP.x - playerP.x;
        float verticalDist = gateP.y - playerP.y;
        if(Mathf.Abs(horizonDist) > Mathf.Abs(verticalDist))
        {
            if(horizonDist > 0)
            {
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
