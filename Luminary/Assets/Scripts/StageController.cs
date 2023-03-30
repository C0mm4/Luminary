using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController
{
    // Start is called before the first frame update



    List<GameObject> rooms;
    public bool[] isClear;
    public bool[] isVIsit;
    public int currentRoom;
    public int roomNo;
    public int stageNo;

    public StageController()
    {
    }
    public void init()
    {
        stageNo = 1;
        setRoom();
    }

    public void nextStage()
    {
        stageNo++;
        setRoom();
    }

    public void setRoom()
    {
        Debug.Log("Starting generating Room");
        int roomNom = 6;
        int roomNoM = 7;

        roomNom += stageNo;
        roomNoM += stageNo * 2;

        roomNo = GameManagers.Random.getGeneralNext(roomNom, roomNoM);
        rooms = GameManagers.MapGen.mapGen(roomNo);

        foreach(GameObject go in rooms)
        {
            go.GetComponent<Room>().set();
        }
        Debug.Log(roomNo);
        if (stageNo == 7)
            roomNo += 2;
        else
            roomNo += 3;
        Debug.Log(roomNo);

        isClear = new bool[roomNo];
        isVIsit = new bool[roomNo];

    }

    public void clear()
    {
        if (rooms != null)
        {
            foreach (GameObject go in rooms)
            {
                GameManagers.Resource.Destroy(go);
            }
            rooms.Clear();
            isClear = new bool[0];
            isVIsit = new bool[0];
        }
    }
}
