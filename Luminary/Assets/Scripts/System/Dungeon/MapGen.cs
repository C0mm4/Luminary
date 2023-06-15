using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MapGen
{
    private int[] xpos;
    private int[] ypos;
    private List<KeyValuePair<int, int>> roomspos;
    private Dictionary<KeyValuePair<int, int>, int> ablepos;
    GameObject dungeon;
    GameObject dungeonRoom;
    GameObject dungeonGate;

    private List<Vector2> disablePos = new List<Vector2>();
    private List<Tuple<Vector2, GameObject, PointPosition>> ableDoorPos = new List<Tuple<Vector2, GameObject, PointPosition>>();

    public void init()
    {
        roomspos = new List<KeyValuePair<int, int>>();
        ablepos = new Dictionary<KeyValuePair<int, int>, int>();
        disablePos = new List<Vector2>();
        //                  U, R, L, D
        xpos = new int[4] { 0, 1, -1, 0 };      
        ypos = new int[4] { 1, 0, 0, -1 };
    }


    public Tuple<List<GameObject>, List<GameObject>> mapGen(int roomNo, int stageNo)
    {
        List<GameObject> ret1 = new List<GameObject>();
        List<GameObject> ret2 = new List<GameObject>();
        KeyValuePair<int, int> target;
        GameObject room = null;
        GameObject tmp = GameObject.Find("Dungeon");
        if (tmp != null)
        {
            GameManager.Resource.Destroy(tmp);
        }

        setParents();

        room = startRoomGen();
        room.GetComponent<Room>().index = 0;
        aadroom(room);
        ret1.Add(room);

        for(int i = 1; i <= roomNo; i++)
        {
            room = startRoomGen();
            room.GetComponent<Room>().index = 1;
            aadroom(room);
            ret1.Add(room);
        }
        Gate[] gts = dungeonGate.gameObject.GetComponentsInChildren<Gate>();
        for(int i = 0; i < gts.Length; i++)
        {
            ret2.Add(gts[i].gameObject);
        }

        return new Tuple<List<GameObject>, List<GameObject>>(ret1, ret2);

        /*



                // Generate StartRoom
                room = startRoomGen();
                addRoom(0,0, room);
                room.GetComponent<Room>().index = 0;
                room.transform.parent = dungeonRoom.transform;
                ret.Add(room);
                // Generate NormalRoom
                for (int i = 0; i < roomNo; i++)
                {
                    target = getRandompos();
                    room = normalRoomGen();
                    addRoom(target.Key, target.Value, room);
                    room.GetComponent<Room>().index = i + 1;
                    room.transform.parent = dungeonRoom.transform;
                    ret.Add(room);
                }
                if(stageNo == 6)
                {

                    // Generate ShopRoom
                    target = getRandompos();
                    room = shopRoomGen();
                    addRoom(target.Key, target.Value, room);
                    room.GetComponent<Room>().index = roomNo + 1;
                    room.transform.parent = dungeonRoom.transform;
                    ret.Add(room);

                    // Generate BossRoom
                    target = getRandompos();
                    room = bossRoomGen();
                    addRoom(target.Key, target.Value, room);
                    room.GetComponent<Room>().index = roomNo + 2;
                    room.transform.parent = dungeonRoom.transform;
                    ret.Add(room);
                }
                else
                {
                    // Generate BossRoom
                    target = getRandompos();
                    room = bossRoomGen();
                    addRoom(target.Key, target.Value, room);
                    room.GetComponent<Room>().index = roomNo + 1;
                    room.transform.parent = dungeonRoom.transform;
                    ret.Add(room);
                }

                return ret;*/
    }

    private void aadroom(GameObject room)
    {
        PointPosition newPos = new PointPosition();
        bool isStart = false;
        // startRoom Generate
        if (ableDoorPos.Count == 0)
        {
            Debug.Log("COUNT = 0");
            room.GetComponent<Room>().x = 0;
            room.GetComponent<Room>().y = 0;
            isStart = true;
        }
        else
        {
            Vector2 rpos = getRndpos(room);
        }
        
        for(int i = 0; i < 4; i++)
        {
            PointPosition door = 0;
            switch (i)
            {
                case 0:
                    if(!isStart)
                    {
                        if(newPos == PointPosition.Up)
                        {
                            continue;
                        }
                    }
                    door = PointPosition.Up;
                    break;
                case 1:
                    if (!isStart)
                    { 
                        if(newPos == PointPosition.Down)
                        {
                            continue;
                        }
                    }
                    door = PointPosition.Down;
                    break;
                case 2:
                    if (!isStart)
                    {
                        if (newPos == PointPosition.Left)
                        {
                            continue;
                        }
                    }
                    door = PointPosition.Left;
                    break;
                case 3:
                    if (!isStart)
                    {
                        if (newPos == PointPosition.Right)
                        {
                            continue;
                        }
                    }
                    door = PointPosition.Right;
                    break;
            }
            Vector2 pos = new Vector2(room.GetComponent<Room>().doorPos[i].x + room.GetComponent<Room>().x, room.GetComponent<Room>().doorPos[i].y + room.GetComponent<Room>().y);
            Debug.Log(pos);
            ableDoorPos.Add(new Tuple<Vector2, GameObject, PointPosition>(pos, room, door));
        }
        int roomleft = room.GetComponent<Room>().doorPos[2].x;
        int roomright = room.GetComponent<Room>().doorPos[3].x;
        int roomup = room.GetComponent<Room>().doorPos[0].y;
        int roomdown = room.GetComponent<Room>().doorPos[1].y;
        for (int i = room.GetComponent<Room>().x + roomleft + 1; i < room.GetComponent<Room>().x + roomright; i++)
        {
            for (int j = room.GetComponent<Room>().y + roomdown + 2; j < room.GetComponent<Room>().y + roomup; j++)
            {
                disablePos.Add(new Vector2(i, j));
            }
        }
    }

    private Vector2 getRndpos(GameObject room)
    {
        int roomleft = room.GetComponent<Room>().doorPos[2].x;
        int roomright = room.GetComponent<Room>().doorPos[3].x;
        int roomup = room.GetComponent<Room>().doorPos[0].y;
        int roomdown = room.GetComponent<Room>().doorPos[1].y;

        Tuple<Vector2, GameObject, PointPosition> tpl;
        int x, y;
        while (true)
        {
            tpl = ableDoorPos[GameManager.Random.getMapNext(0, ableDoorPos.Count)];
            x = (int)tpl.Item1.x;
            y = (int)tpl.Item1.y;
            switch (tpl.Item3)
            {
                case PointPosition.Up:
                    x -= room.GetComponent<Room>().doorPos[1].x;
                    y -= room.GetComponent<Room>().doorPos[1].y;
                    break;
                case PointPosition.Down:
                    x -= room.GetComponent<Room>().doorPos[0].x;
                    y -= room.GetComponent<Room>().doorPos[0].y;
                    break;
                case PointPosition.Left:
                    x -= room.GetComponent<Room>().doorPos[3].x;
                    y -= room.GetComponent<Room>().doorPos[3].y;
                    break;
                case PointPosition.Right:
                    x -= room.GetComponent<Room>().doorPos[2].x;
                    y -= room.GetComponent<Room>().doorPos[2].y;
                    break;
            }
            if (!disablePos.Contains(new Vector2(x, y)) && !disablePos.Contains(new Vector2(x + roomleft, y + roomup))
    && !disablePos.Contains(new Vector2(x + roomright, y + roomup)) && !disablePos.Contains(new Vector2(x + roomleft, y + roomdown))
    && !disablePos.Contains(new Vector2(x + roomright, y + roomdown)))
                break;
        }

        GameObject go = GameManager.Resource.Instantiate("Dungeon/Gate", dungeonGate.transform);
        go.transform.position = new Vector3(tpl.Item1.x, tpl.Item1.y, 2);

        room.GetComponent<Room>().x = x;
        room.GetComponent<Room>().y = y;

        Vector2 ret = new Vector2(x, y);
        ableDoorPos.Remove(tpl);
        return ret;
    }

    private void setParents()
    {
        dungeon = new GameObject("Dungeon");
        dungeonRoom = new GameObject("rooms");
        dungeonGate = new GameObject("gates");
        dungeonRoom.transform.parent = dungeon.transform;
        dungeonGate.transform.parent = dungeon.transform;
    }

    // Return created room prefab
    private GameObject startRoomGen()
    {
        GameObject room = GameManager.Resource.Instantiate("Dungeon/NewRoomsForm", dungeonRoom.transform);
        return room;
    }
    private GameObject normalRoomGen()
    {
        GameObject room = GameManager.Resource.Instantiate("Dungeon/NormalRoom", dungeonRoom.transform);
        return room;
    }
    private GameObject shopRoomGen()
    {
        GameObject room = GameManager.Resource.Instantiate("Dungeon/ShopRoom", dungeonRoom.transform);
        return room;
    }
    private GameObject bossRoomGen()
    {
        GameObject room = GameManager.Resource.Instantiate("Dungeon/BossRoom", dungeonRoom.transform);
        return room;
    }

    // When New Dungeon Create or Next Dungeon Create buffer Clear
    public void clear()
    {
        roomspos.Clear();
        ablepos.Clear();
        disablePos.Clear();
        ableDoorPos.Clear();
    }

    // Filled new Room Object in Buffer and able new Position Add
    private void addRoom(int x, int y, GameObject obj)
    {
        // °´Ã¼ x,y Ä­ ¼³Á¤
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
    
    // O(n^2) Create Gates between Rooms
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
                        GameObject gate = GameManager.Resource.Instantiate("Dungeon/Gate");
                        gate.GetComponent<Gate>().index = gatecnt;
                        Char[] strI = rms.ElementAt(i).GetComponent<Room>().gatedir.ToCharArray();
                        Char[] strJ = rms.ElementAt(j).GetComponent<Room>().gatedir.ToCharArray();
                        switch (k)
                        {
                            case 0:
                                rms.ElementAt(i).GetComponent<Room>().gateU = gatecnt;
                                strI[3] = '1';
                                rms.ElementAt(j).GetComponent<Room>().gateD = gatecnt;
                                strJ[2] = '1';
                                break;
                            case 1:
                                rms.ElementAt(i).GetComponent<Room>().gateR = gatecnt;
                                strI[0] = '1';
                                rms.ElementAt(j).GetComponent<Room>().gateL = gatecnt;
                                strJ[1] = '1';
                                break;
                            case 2:
                                rms.ElementAt(i).GetComponent<Room>().gateL = gatecnt;
                                strI[1] = '1';
                                rms.ElementAt(j).GetComponent<Room>().gateR = gatecnt;
                                strJ[0] = '1';
                                break;
                            case 3:
                                rms.ElementAt(i).GetComponent<Room>().gateD = gatecnt;
                                strI[2] = '1';
                                rms.ElementAt(j).GetComponent<Room>().gateU = gatecnt;
                                strJ[3] = '1';
                                break;
                        }

                        gate.GetComponent<Gate>().posx = (float)(currentx + nextx) / 2;
                        gate.GetComponent<Gate>().posy = (float)(currenty + nexty) / 2;

                        rms.ElementAt(i).GetComponent<Room>().gatedir = new string(strI);
                        rms.ElementAt(j).GetComponent<Room>().gatedir = new string(strJ);

                        gate.GetComponent<Gate>().room1 = rms.ElementAt(i).GetComponent<Room>().index;
                        gate.GetComponent<Gate>().room2 = rms.ElementAt(j).GetComponent<Room>().index;
                        gate.transform.parent = dungeonGate.transform;
                        ret.Add(gate);
                        gatecnt++;
                    }

                }
            }
        }
        return ret;
    }

    // return Random Position In abldpos buffer
    private KeyValuePair<int, int> getRandompos()
    {
        int acount = ablepos.Count();
        KeyValuePair<KeyValuePair<int, int>, int> res = ablepos.ElementAt(GameManager.Random.getMapNext(0, acount));
        while (res.Value >= 3)
        {
            res = ablepos.ElementAt(GameManager.Random.getMapNext(0, acount));
        }
        return res.Key;
    }
}
