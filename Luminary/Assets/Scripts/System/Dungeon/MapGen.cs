using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MapGen
{
    private int[] xpos;
    private int[] ypos;
    private List<KeyValuePair<int, int>> roomspos;
    private Dictionary<Vector2, KeyValuePair<DunRoom, PointPosition>> ablepos;
    GameObject dungeon;
    GameObject dungeonRoom;
    GameObject dungeonGate;

    private List<Vector2> disablePos = new List<Vector2>();
    private List<Tuple<Vector2, GameObject, PointPosition>> ableDoorPos = new List<Tuple<Vector2, GameObject, PointPosition>>();

    private List<KeyValuePair<Vector2, int>> positionData = new List<KeyValuePair<Vector2, int>>();

    public void init()
    {
        roomspos = new List<KeyValuePair<int, int>>();
        ablepos = new Dictionary<Vector2, KeyValuePair<DunRoom, PointPosition>>();
        disablePos = new List<Vector2>();
        //                  U, R, L, D
        xpos = new int[4] { 0, 1, -1, 0 };      
        ypos = new int[4] { 1, 0, 0, -1 };
    }

    public void DungeonGen()
    {
        positionData.Clear();

        dungeon = new GameObject();

        DunRoom room = new DunRoom();
        StartRoomGen();

        for (int i = 1; i < 20; i++)
        {
            room = DungeonRoomGen();
            room.roomID = i;

        }

        GameManager.Instance.playerGen();
    }

    public void test()
    {
        DungeonRoomGen();
        GameObject go = GameObject.Find("a");
        GameManager.Resource.Destroy(go);


    }

    public DunRoom StartRoomGen()
    {
        DunRoom room = GameManager.Resource.Instantiate("Dungeon/Room/StartRoom", dungeon.transform).GetComponent<DunRoom>();
        room.transform.position = new Vector3(0, 0, 2);
        room.roomID = 0;
        room.x = 0;
        room.y = 0;
        SetTilePos(room);
        SetPosData(room);
        return room;
    }
    /*

    public DunRoom BossRoomGen()
    {

    }

    public DunRoom ShopRoomGen()
    {

    }
    */
    public DunRoom DungeonRoomGen()
    {
        DunRoom room = GameManager.Resource.Instantiate("Dungeon/Room/StartRoom", dungeon.transform).GetComponent<DunRoom>();
        Vector2 pos = new Vector2();

        List<Vector2> keys = ablepos.Keys.ToList();
        PointPosition po;
        KeyValuePair<DunRoom, PointPosition> targetRoom;
        do
        {
            int randIndex = GameManager.Random.getMapNext(0, keys.Count);
            pos = keys[randIndex];
            targetRoom = ablepos[pos];
            Vector2 targetPos = new Vector2(targetRoom.Key.x, targetRoom.Key.y);
            po = Func.GetPointPosition(targetPos, pos);
            switch (po)
            {
                case PointPosition.Up:
                    room.y = (int)pos.y + room.centerY;
                    room.x = (int)pos.x;
                    break;
                case PointPosition.Down:
                    room.y = (int)pos.y - (room.sizeY - room.centerY - 1);
                    room.x = (int)pos.x;
                    break;
                case PointPosition.Left:
                    room.x = (int)pos.x - (room.sizeX - room.centerX - 1);
                    room.y = (int)pos.y;
                    break;
                case PointPosition.Right:
                    room.x = (int)pos.x + room.centerX;
                    room.y = (int)pos.y;
                    break;
            }

        }
        while (CheckAblePos(room));

        var deltarget = ablepos.Where(pair => pair.Value.Key == targetRoom.Key && pair.Value.Value == po);
        


        foreach(var pair in deltarget.ToList())
        {
            Debug.Log(pair.Key);
            ablepos.Remove(pair.Key);
        }

        room.transform.position = new Vector3(room.x * 2.56f, room.y * 2.56f, 2);
        SetTilePos(room);
        SetDoorTile(targetRoom.Key, room, pos, po);
        SetPosData(room, po);



        return room;
    }

    public void SetDoorTile(DunRoom sroom, DunRoom troom, Vector2 pos, PointPosition po)
    {
        int type = new int();
        switch (po)
        {
            case PointPosition.Left:
                type = 4;
                break;
            case PointPosition.Right:
                type = 6;
                break;
            case PointPosition.Up:
                type = 2;
                break;
            case PointPosition.Down:
                type = 8;
                break;

        }
        List<Tile> targetTiles = new List<Tile>();
        foreach(Tile tile in sroom.tiles)
        {
            if(tile.types == type)
            {
                targetTiles.Add(tile);
            }
        }

        Tile target = targetTiles[GameManager.Random.getMapNext(0, targetTiles.Count)];
        int t = type / 2 - 1;

        GameObject go = GameManager.Resource.Instantiate(sroom.doorTiles[t], sroom.Tiles.transform);
        go.transform.position = target.transform.position;
        go.GetComponent<Tile>().x = target.x;
        go.GetComponent<Tile>().y = target.y;
        int index = sroom.tiles.FindIndex(tile => tile == target);

        sroom.tiles[index] = go.GetComponent<Tile>();

        GameManager.Resource.Destroy(target.gameObject);
        type = 10 - type;
        
        Tile troomTile = troom.tiles.Find(tile => tile.x == pos.x && tile.y == pos.y);
        if (troomTile != null)
        {
            go = GameManager.Resource.Instantiate(troom.doorTiles[type / 2 - 1], troom.Tiles.transform);
            go.transform.position = troomTile.transform.position;
            go.GetComponent<Tile>().x = (int)pos.x;
            go.GetComponent<Tile>().y = (int)pos.y;
            GameManager.Resource.Destroy(troomTile.gameObject);
        }
        else
        {
            Debug.Log(troom.roomID);
            Debug.Log(pos.x + " " + pos.y);
        }

        SetCorridor(new Vector2(target.x, target.y), new Vector2(troomTile.x, troomTile.y), po);


    }

    public void SetTilePos(DunRoom room)
    {
        for (int i = 0; i < room.tiles.Count; i++)
        {
            Vector3 pos = room.tiles[i].transform.position;
            float x = pos.x / 2.56f;
            float y = pos.y / 2.56f;
            room.tiles[i].x = (int)Math.Round(x);
            room.tiles[i].y = (int)Math.Round(y);

        }
    }

    public void SetPosData(DunRoom room, PointPosition dir = PointPosition.Null)
    {
        for (int i = room.x - room.centerX - 4; i < room.x + room.sizeX - room.centerX + 4; i++)
        {
            for(int j = room.y - room.centerY - 4; j <  room.y + room.sizeY - room.centerY + 4; j++)
            {
                Vector2 pos;
                pos = new Vector2(i, j);

                if ((i == room.x - room.centerX - 4 || i == room.x + room.sizeX - room.centerX + 3) || (j == room.y - room.centerY - 4 || j == room.y + room.sizeY - room.centerY + 3))
                {
                    if (ablepos.ContainsKey(pos))
                    {
                        ablepos.Remove(pos);
                        disablePos.Add(pos);
                    }
                    else
                    {
                        if (disablePos.FindIndex(vector => vector == pos) == -1)
                        {
                            PointPosition po = Func.GetPointPosition(new Vector2(room.x, room.y), pos);
                            KeyValuePair<DunRoom, PointPosition> keyValuePair = new KeyValuePair<DunRoom, PointPosition>(room, po);
                            if(dir == PointPosition.Null)
                            {
                                ablepos[pos] = keyValuePair;
                            }
                            else
                            {
                                switch (dir)
                                {
                                    case PointPosition.Left:
                                        if(po != PointPosition.Right)
                                        {

                                            ablepos[pos] = keyValuePair;
                                        }
                                        break;
                                    case PointPosition.Right:
                                        if(po != PointPosition.Left)
                                        {

                                            ablepos[pos] = keyValuePair;
                                        }
                                        break;
                                    case PointPosition.Up:
                                        if(po != PointPosition.Down)
                                        {

                                            ablepos[pos] = keyValuePair;
                                        }
                                        break;
                                    case PointPosition.Down:
                                        if(po != PointPosition.Up)
                                        {

                                            ablepos[pos] = keyValuePair;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ablepos.ContainsKey(pos))
                    {
                        ablepos.Remove(pos);

                    }
                    disablePos.Add(pos);
                }
            }
        }
    }

    public bool CheckAblePos(DunRoom room)
    {
        for (int i = room.x - room.centerX; i < room.x + room.sizeX - room.centerX - 1; i++)
        {
            for (int j = room.y - room.centerY; j < room.y + room.sizeY - room.centerY - 1; j++)
            {
                Vector2 pos = new Vector2(i, j);
                if (disablePos.FindIndex(vector => vector == pos) != -1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void SetCorridor(Vector2 startPos, Vector2 targetPos, PointPosition dir)
    {
        List<KeyValuePair<Vector2, int>> corridor = new List<KeyValuePair<Vector2, int>>();
        Vector2 pos = new Vector2();
        pos.x = startPos.x;
        pos.y = startPos.y;
        if(dir == PointPosition.Up || dir == PointPosition.Down)
        {
            int centerY = ((int)targetPos.y - (int)startPos.y)/2 + (int)startPos.y;
            if (dir == PointPosition.Up)
            {
                for(pos.y = startPos.y + 1; pos.y < centerY; pos.y++)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 6);
                    corridor.Add(tile);
                }
                
                if(startPos.x > targetPos.x)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 2);
                    corridor.Add(tile);

                    for(pos.x--; pos.x > targetPos.x; pos.x--)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 1);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 5);
                    corridor.Add(tile);
                }
                else if(startPos.x < targetPos.x)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 4);
                    corridor.Add(tile);

                    for (pos.x++; pos.x < targetPos.x; pos.x++)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 1);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 3);
                    corridor.Add(tile);
                }
                else
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 6);
                    corridor.Add(tile);
                }
                
                for(pos.y++; pos.y < targetPos.y; pos.y++)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 6);
                    corridor.Add(tile);
                }
                
            }
            else
            {
                for (pos.y = startPos.y - 1; pos.y > centerY; pos.y--)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 6);
                    corridor.Add(tile);
                }

                if (startPos.x > targetPos.x)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 3);
                    corridor.Add(tile);

                    for (pos.x--; pos.x > targetPos.x; pos.x--)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 1);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 4);
                    corridor.Add(tile);
                }
                else if (startPos.x < targetPos.x)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 5);
                    corridor.Add(tile);

                    for (pos.x++; pos.x < targetPos.x; pos.x++)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 1);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 2);
                    corridor.Add(tile);
                }
                else
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 6);
                    corridor.Add(tile);
                }

                for (pos.y--; pos.y > targetPos.y; pos.y--)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 6);
                    corridor.Add(tile);
                }
            }
        }
        else
        {
            int centerX = ((int)targetPos.x - (int)startPos.x)/2 + (int)startPos.x;
            if(dir == PointPosition.Right)
            {
                for (pos.x = startPos.x + 1; pos.x < centerX; pos.x++)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 1);
                    corridor.Add(tile);
                }

                if (startPos.y > targetPos.y)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 2);
                    corridor.Add(tile);

                    for (pos.y--; pos.y > targetPos.y; pos.y--)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 6);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 5);
                    corridor.Add(tile);
                }
                else if (startPos.y < targetPos.y)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 3);
                    corridor.Add(tile);

                    for (pos.y++; pos.y < targetPos.y; pos.y++)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 6);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 4);
                    corridor.Add(tile);
                }
                else
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 1);
                    corridor.Add(tile);
                }

                for (pos.x++; pos.x < targetPos.x; pos.x++)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 1);
                    corridor.Add(tile);
                }
            }
            else
            {
                for (pos.x = startPos.x - 1; pos.x > centerX; pos.x--)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 1);
                    corridor.Add(tile);
                }

                if (startPos.y > targetPos.y)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 4);
                    corridor.Add(tile);

                    for (pos.y--; pos.y > targetPos.y; pos.y--)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 6);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 3);
                    corridor.Add(tile);
                }
                else if (startPos.y < targetPos.y)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 5);
                    corridor.Add(tile);

                    for (pos.y++; pos.y < targetPos.y; pos.y++)
                    {
                        tile = new KeyValuePair<Vector2, int>(pos, 6);
                        corridor.Add(tile);
                    }

                    tile = new KeyValuePair<Vector2, int>(pos, 2);
                    corridor.Add(tile);
                }
                else
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 1);
                    corridor.Add(tile);
                }

                for (pos.x--; pos.x > targetPos.x; pos.x--)
                {
                    KeyValuePair<Vector2, int> tile = new KeyValuePair<Vector2, int>(pos, 1);
                    corridor.Add(tile);
                }
            }
        }

        GameObject go = new GameObject();
        foreach (KeyValuePair<Vector2, int> tile in corridor)
        {
            switch (tile.Value)
            {
                case 1:
                    go = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_Column", dungeon.transform);
                    break;
                case 2:
                    go = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_LeftDown", dungeon.transform);
                    break;
                case 3:
                    go = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_LeftUp", dungeon.transform);
                    break;
                case 4:
                    go = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_RightDown", dungeon.transform);
                    break;
                case 5:
                    go = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_RightUp", dungeon.transform);
                    break;
                case 6:
                    go = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_Vertical", dungeon.transform);
                    break;

            }

            go.transform.position = new Vector3(tile.Key.x * 2.56f, tile.Key.y * 2.56f, 2);
            go.GetComponent<Tile>().x = (int)Math.Round(tile.Key.x);
            go.GetComponent<Tile>().y = (int)Math.Round(tile.Key.y);
            if (ablepos.ContainsKey(pos))
            {
                ablepos.Remove(pos);
            }
            disablePos.Add(pos);

        }
    }
/*
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
        addroom(room);
        ret1.Add(room);

        for(int i = 1; i <= roomNo; i++)
        {
            room = normalRoomGen();
            room.GetComponent<Room>().index = i;
            addroom(room);
            ret1.Add(room);
        }
        Gate[] gts = dungeonGate.gameObject.GetComponentsInChildren<Gate>();
        for(int i = 0; i < gts.Length; i++)
        {
            ret2.Add(gts[i].gameObject);
        }

        return new Tuple<List<GameObject>, List<GameObject>>(ret1, ret2);

    }

    private void addroom(GameObject room)
    {
        PointPosition newPos = new PointPosition();
        bool isStart = false;
        // startRoom Generate
        if (ableDoorPos.Count == 0)
        {
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
                    room.GetComponent<Room>().gateU = true;
                    tpl.Item2.GetComponent<Room>().gateD = true;
                    break;
                case PointPosition.Down:
                    x -= room.GetComponent<Room>().doorPos[0].x;
                    y -= room.GetComponent<Room>().doorPos[0].y;
                    room.GetComponent<Room>().gateD = true;
                    tpl.Item2.GetComponent<Room>().gateU = true;
                    break;
                case PointPosition.Left:
                    x -= room.GetComponent<Room>().doorPos[3].x;
                    y -= room.GetComponent<Room>().doorPos[3].y;
                    room.GetComponent<Room>().gateL = true;
                    tpl.Item2.GetComponent<Room>().gateR = true;
                    break;
                case PointPosition.Right:
                    x -= room.GetComponent<Room>().doorPos[2].x;
                    y -= room.GetComponent<Room>().doorPos[2].y;
                    room.GetComponent<Room>().gateR = true;
                    tpl.Item2.GetComponent<Room>().gateL = true;
                    break;
            }
            if (!disablePos.Contains(new Vector2(x, y)) && !disablePos.Contains(new Vector2(x + roomleft, y + roomup))
    && !disablePos.Contains(new Vector2(x + roomright, y + roomup)) && !disablePos.Contains(new Vector2(x + roomleft, y + roomdown))
    && !disablePos.Contains(new Vector2(x + roomright, y + roomdown)))
                break;
        }

        GameObject go = GameManager.Resource.Instantiate("Dungeon/Gate", dungeonGate.transform);
        go.transform.position = new Vector3(tpl.Item1.x + 0.5f, tpl.Item1.y + 0.5f, 2);
        go.GetComponent<Gate>().room1 = tpl.Item2.GetComponent<Room>().index;
        go.GetComponent<Gate>().room2 = room.GetComponent<Room>().index;

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
        GameObject room = GameManager.Resource.Instantiate("Dungeon/Room0", dungeonRoom.transform);
        return room;
    }
    private GameObject normalRoomGen()
    {
        int rnd = GameManager.Random.getMapNext(0, 4);
        GameObject room = GameManager.Resource.Instantiate("Dungeon/Room"+rnd, dungeonRoom.transform);
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

*/
    // When New Dungeon Create or Next Dungeon Create buffer Clear
    public void clear()
    {
        roomspos.Clear();
        ablepos.Clear();
        disablePos.Clear();
        ableDoorPos.Clear();
    }

}
