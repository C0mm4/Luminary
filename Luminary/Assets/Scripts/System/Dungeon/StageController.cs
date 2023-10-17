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


    public List<DunRoom> rooms;
    public List<GameObject> gates;
    public bool[] isClear;
    public bool[] isVIsit;
    public int currentRoom;
    public int roomNo;
    public int stageNo;


    public bool isTutorial = false;


    // When Game Starts Create Stage 1 Dungeon
    public void init()
    {

    }

    public void tutorial()
    {/*
        GameObject objs = GameObject.Find("TutorialManager");
        rooms = new List<DunRoom>();
        foreach(GameObject obj in objs.GetComponent<TutorialManager>().rooms) 
        { 
            rooms.Add(obj.GetComponent<DunRoom>());
            Room room = obj.GetComponent<Room>();
            Transform[] enemies = room.enemies.transform.GetComponentsInChildren<Transform>().Where(child => child.CompareTag("Mob")).ToArray();
            foreach(Transform enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        isVIsit = new bool[rooms.Count];
        isClear = new bool[rooms.Count];
        isTutorial = true;*/
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

        int roomNom = 6;
        int roomNoM = 7;

        roomNom += stageNo;
        roomNoM += stageNo * 2;
        int roomN = GameManager.Random.getMapNext(roomNom, roomNoM);

        rooms = GameManager.MapGen.DungeonGen(roomN);

        if (GameObject.Find("PlayerbleChara"))
        {
            GameManager.Resource.Destroy(GameObject.Find("PlayerbleChara"));
        }
        
    }



    // Clear Buffer
    public void clear()
    {
        if (rooms != null)
        {
            foreach (DunRoom go in rooms)
            {
                GameManager.Resource.Destroy(go.gameObject);
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
    // move n room and start room
    public void moveRoom(int n)
    {
        currentRoom = n;
        if (isTutorial)
        {
            Debug.Log("ISTUTORIAL");

            rooms[currentRoom].GetComponent<DunRoom>().ActivateRoom();
        }
        else if (!rooms[n].isActivate)
        {
            rooms[currentRoom].GetComponent<DunRoom>().ActivateRoom();
        }
        rooms[currentRoom].isActivate = true;
    }

    public void ClearRoom()
    {
        rooms[currentRoom].GetComponent <DunRoom>().isClear = true;
        rooms[currentRoom].GetComponent<DunRoom>().OpenDoor();
    }

}
