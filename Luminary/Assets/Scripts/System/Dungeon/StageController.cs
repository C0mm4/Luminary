using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
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

    XmlDocument mapDatas;
    XmlNodeList roomDatas;

    public bool isTutorial = false;


    // When Game Starts Create Stage 1 Dungeon
    public void init()
    {
        mapDatas = GameManager.Resource.LoadXML("MapsXML");
        roomDatas = mapDatas.GetElementsByTagName("Room");
    }

    public void tutorial()
    {
        GameObject objs = GameObject.Find("TutorialManager");
        rooms = new List<GameObject>();
        foreach(GameObject obj in objs.GetComponent<TutorialManager>().rooms) 
        { 
            rooms.Add(obj);
            Room room = obj.GetComponent<Room>();
            Transform[] enemies = room.enemies.transform.GetComponentsInChildren<Transform>().Where(child => child.CompareTag("Mob")).ToArray();
            foreach(Transform enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        isVIsit = new bool[rooms.Count];
        isClear = new bool[rooms.Count];
        isTutorial = true;
    }

    public void gameStart()
    {
        isTutorial = false;
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
        if (GameObject.Find("PlayerbleChara"))
        {
            GameManager.Resource.Destroy(GameObject.Find("PlayerbleChara"));
        }

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
        (rooms, gates) = GameManager.MapGen.mapGen(roomNo, stageNo);

        foreach(GameObject go in rooms)
        {
            go.GetComponent<Room>().set();
        }
        if (stageNo == 7)
            roomNo += 2;
        else
            roomNo += 3;

        isClear = new bool[roomNo];
        isVIsit = new bool[roomNo];

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

    public void moveRoom(int n)
    {
        currentRoom = n;
        GameManager.cameraManager.background = rooms[currentRoom].GetComponent<Room>().bg;
        if (isTutorial)
        {
            Debug.Log("ISTUTORIAL");

            rooms[currentRoom].GetComponent<Room>().ActiveEnemies();
        }
        else if (!isVIsit[currentRoom])
        {
            rooms[currentRoom].GetComponent<Room>().ActiveEnemies();
        }
        if (rooms[currentRoom].GetComponent<Room>().mobCount == 0)
        {
            isClear[currentRoom] = true;
            rooms[currentRoom].GetComponent<Room>().clearRoom();
        }
        isVIsit[currentRoom] = true;
    }

    public void closeDoor()
    {
        if (!isClear[currentRoom])
        {
            foreach (GameObject gate in gates)
            {
                if (gate != null)
                {
                    gate.GetComponent<Gate>().closeGate();
                    //gate.SetActive(true);
                }
            }
        }
    }

    public void openDoor()
    {
        foreach(GameObject gate in gates)
        {
            if (gate != null)
            {
                gate.GetComponent<Gate>().openGate();
                //gate.SetActive(false);
            }
        }
    }
}
