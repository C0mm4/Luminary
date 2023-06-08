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
        setGate();
        setRoomCompo();
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
        rooms = GameManager.MapGen.mapGen(roomNo, stageNo);

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

    // Set Room Grid
    public void setRoomCompo()
    {
        foreach(GameObject go in rooms)
        {
            List<int> lists = new List<int>();
            Room r = go.GetComponent<Room>();
            char[] str = r.gatedir.ToCharArray();
            foreach(XmlNode node in roomDatas)
            {
                char[] mopen = node["open"].InnerText.ToCharArray();
                int i = 0;
                for(; i < 4; i++)
                {
                    if (str[i] == '1')
                    {
                        if (mopen[i] == '1')
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if(i == 4)
                {
                    lists.Add(int.Parse(node["id"].InnerText));
                }
            }
            int target = GameManager.Random.getMapNext(0,lists.Count);
            string ary = roomDatas[target]["data"].InnerText;

            ary = ary.Substring(6);
            ary = ary.Replace("\t\t\t", string.Empty);
            ary = ary.Replace("\n", string.Empty); 
            string[] sstr = ary.Split('\t');
            int[] iary = Array.ConvertAll(sstr,  s=> int.TryParse(s, out var x) ? x : -1);
            
            go.GetComponent<Room>().setData(iary);

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

    public void moveRoom(int n)
    {
        currentRoom = n;
        if (isTutorial)
        {
            Debug.Log("ISTUTORIAL");

            rooms[currentRoom].GetComponent<Room>().ActiveEnemies();
        }
        else if (!isVIsit[currentRoom])
        {
            rooms[currentRoom].GetComponent<Room>().setObjects();
        }
        if (rooms[currentRoom].GetComponent<Room>().mobCount == 0)
        {
            isClear[currentRoom] = true;
            rooms[currentRoom].GetComponent<Room>().clearRoom();
        }
        isVIsit[currentRoom] = true;
        GameManager.cameraManager.background = rooms[currentRoom].GetComponent<Room>().bg;
    }


}
