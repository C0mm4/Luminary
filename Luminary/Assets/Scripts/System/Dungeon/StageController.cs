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
            Debug.Log(go.name);
            Room r = go.GetComponent<Room>();

            char[] str = r.gatedir.ToCharArray();
            int target;
            if (r.types == 1)
            {

                List<int> lists = new List<int>();
                foreach (XmlNode node in roomDatas)
                {
                    char[] mopen = node["open"].InnerText.ToCharArray();
                    char[] comp = { '0', '0', '0', '0' };
                    for(int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == mopen[i])
                        {
                            comp[i] = str[i];
                        }
                        else
                        {
                            comp[i] = '0';
                        }
                    }
                    if (str.SequenceEqual(comp))
                    {
                        lists.Add(int.Parse(node["id"].InnerText));
                        Debug.Log(int.Parse(node["id"].InnerText));
                    }
                }
                target = lists[GameManager.Random.getMapNext(0, lists.Count)];
                lists.Clear();
            }
            else
            {
                target = 0;
            }
            
            string ary = roomDatas[target]["data"].InnerText;
            Debug.Log(target);

            ary = ary.Substring(6);
            ary = ary.Replace("\t\t\t", string.Empty);
            ary = ary.Replace("\n", string.Empty); 
            string[] sstr = ary.Split('\t');
            int[] iary = Array.ConvertAll(sstr,  s=> int.TryParse(s, out var x) ? x : -1);
            
            go.GetComponent<Room>().setData(iary);
            go.GetComponent<Room>().setObjects();
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

}
