using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController
{


    public List<GameObject> rooms;
    public List<GameObject> gates;
    public bool[] isClear;
    public bool[] isVIsit;
    public int currentRoom;
    public int roomNo;
    public int stageNo;

    public StageController()
    {
    }

    // When Game Starts Create Stage 1 Dungeon
    public void init()
    {
        stageNo = 1;
        startStage();
    }

    // When Next Stage trigger begins Create Next Stage Dungeion.
    public void nextStage()
    {
        stageNo++;
        startStage();
    }

    // Dungeon Create
    private void startStage()
    {
        setRoom();
        setGate();
        if (GameObject.Find("PlayerbleChara"))
        {
            GameManager.Resource.Destroy(GameObject.Find("PlayerbleChara"));
        }
        GameManager.Instance.playerGen();
    }

    // Create Rooms
    public void setRoom()
    {
        Debug.Log("Starting generating Room");
        int roomNom = 6;
        int roomNoM = 7;

        roomNom += stageNo;
        roomNoM += stageNo * 2;

        roomNo = GameManager.Random.getGeneralNext(roomNom, roomNoM);
        rooms = GameManager.MapGen.mapGen(roomNo);

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

    // Create Gates
    public void setGate()
    {
        gates = GameManager.MapGen.setGates(rooms);
        foreach(GameObject go in gates)
        {
            go.GetComponent<Gate>().set();
        }
    }

    // Clear Buffer
    public void clear()
    {
        if (rooms != null)
        {
            foreach (GameObject go in rooms)
            {
                GameManager.Resource.Destroy(go);
            }
            rooms.Clear();
            foreach (GameObject go in gates)
            {
                GameManager.Resource.Destroy(go);
            }
            gates.Clear();
            isClear = new bool[0];
            isVIsit = new bool[0];
        }
    }
}
