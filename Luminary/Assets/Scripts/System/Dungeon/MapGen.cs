using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGen
{
    private int[] xpos;
    private int[] ypos;
    private List<KeyValuePair<int, int>> roomspos;
    private Dictionary<KeyValuePair<int, int>, int> ablepos;


    public MapGen()
    {
        roomspos = new List<KeyValuePair<int, int>>();
        ablepos = new Dictionary<KeyValuePair<int, int>, int>();
        //                  U, R, L, D
        xpos = new int[4] { 0, 1, -1, 0 };      
        ypos = new int[4] { 1, 0, 0, -1 };
    }


    public List<GameObject> mapGen(int roomNo)
    {
        List<GameObject> ret = new List<GameObject>();
        KeyValuePair<int, int> target;

        GameObject room = null;
        
        // 시작 방 생성
        room = startRoomGen();
        addRoom(0,0, room);
        room.GetComponent<Room>().index = 0; 
        ret.Add(room);
        // 일반(파밍 방 생성)
        for (int i = 0; i < roomNo; i++)
        {
            target = getRandompos();
            room = normalRoomGen();
            addRoom(target.Key, target.Value, room);
            room.GetComponent<Room>().index = i + 1;
            ret.Add(room);
        }

        // 상점 방 생성
        target = getRandompos();
        room = shopRoomGen();
        addRoom(target.Key, target.Value, room);
        ret.Add(room);

        // 보스 방 생성
        target = getRandompos();
        room = bossRoomGen();
        addRoom(target.Key, target.Value, room);
        room.GetComponent<Room>().index = roomNo + 1;
        ret.Add(room);

        return ret;
    }


    private GameObject startRoomGen()
    {
        // 시작 맵 게임 오브젝트 생성 및 반환
        GameObject room = GameManagers.Resource.Instantiate("Room");
        return room;
    }
    private GameObject normalRoomGen()
    {
        GameObject room = GameManagers.Resource.Instantiate("Room");
        return room;
    }
    private GameObject shopRoomGen()
    {
        GameObject room = GameManagers.Resource.Instantiate("Room");
        return room;
    }
    private GameObject bossRoomGen()
    {
        GameObject room = GameManagers.Resource.Instantiate("Room");
        return room;
    }

    public void clear()
    {
        roomspos.Clear();
        ablepos.Clear();
    }

    private void addRoom(int x, int y, GameObject obj)
    {
        // 객체 x,y 칸 설정
        obj.GetComponent<Room>().x = x;
        obj.GetComponent<Room>().y = y;
        roomspos.Add(new KeyValuePair<int, int>(x, y));
        for(int i = 0; i < 4; i++)
        {
            KeyValuePair<int, int> target = new KeyValuePair<int, int>(x + xpos[i], y + ypos[i]);
            if(roomspos.IndexOf(new KeyValuePair<int, int>(target.Key, target.Value)) == -1)
            {
                if (!ablepos.ContainsKey(target))
                {
                    ablepos.Add(target, 1);
                }
                else
                {
                    ablepos[target] += 1;
                }
            }
        }
        ablepos.Remove(new KeyValuePair<int, int>(x, y));
    }
    
    public List<GameObject> setGates(List<GameObject> rms)
    {
        List<GameObject> ret = new List<GameObject>();
        int cnt = rms.Count();
        int gatecnt = 0;
        for (int i = 0; i < cnt -1; i++)
        {
            int currentx = rms.ElementAt(i).GetComponent<Room>().x;
            int currenty = rms.ElementAt(i).GetComponent<Room>().y;
            for(int j = i+1; j < cnt; j++)
            {
                int nextx = rms.ElementAt(j).GetComponent<Room>().x;
                int nexty = rms.ElementAt (j).GetComponent<Room>().y;

                for (int k = 0; k < 4; k++)
                {
                    if (currentx + xpos[k] == nextx && currenty + ypos[k] == nexty)
                    {
                        GameObject gate = GameManagers.Resource.Instantiate("Gate");
                        gate.GetComponent<Gate>().index = gatecnt;
                        switch (k)
                        {
                            case 0:
                                rms.ElementAt(i).GetComponent<Room>().gateU = gatecnt;
                                rms.ElementAt(j).GetComponent<Room>().gateD = gatecnt;
                                break;
                            case 1:
                                rms.ElementAt(i).GetComponent<Room>().gateR = gatecnt;
                                rms.ElementAt(j).GetComponent<Room>().gateL = gatecnt;
                                break;
                            case 2:
                                rms.ElementAt(i).GetComponent<Room>().gateL = gatecnt;
                                rms.ElementAt(j).GetComponent<Room>().gateR = gatecnt;
                                break;
                            case 3:
                                rms.ElementAt(i).GetComponent<Room>().gateD = gatecnt;
                                rms.ElementAt(j).GetComponent<Room>().gateU = gatecnt;
                                break;
                        }

                        gate.GetComponent<Gate>().posx = (float)(currentx + nextx) / 2;
                        gate.GetComponent<Gate>().posy = (float)(currenty + nexty) / 2;


                        gate.GetComponent<Gate>().room1 = rms.ElementAt(i).GetComponent<Room>().index;
                        gate.GetComponent<Gate>().room2 = rms.ElementAt(j).GetComponent<Room>().index;
                        ret.Add(gate);
                        gatecnt++;
                    }

                }
            }
        }
        return ret;
    }

    private KeyValuePair<int, int> getRandompos()
    {
        int acount = ablepos.Count();
        KeyValuePair<KeyValuePair<int, int>, int> res = ablepos.ElementAt(GameManagers.Random.getMapNext(0, acount));
        while (res.Value >= 3)
        {
            res = ablepos.ElementAt(GameManagers.Random.getMapNext(0, acount));
        }
        return res.Key;
    }
}
