using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
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

    GameObject dungeon;
    GameObject roomObj;
    GameObject corridorObj;
    public GameObject bg;
    public GameObject Doors;

    private List<Vector2> disablePos = new List<Vector2>();
    private Dictionary<Vector2, KeyValuePair<DunRoom, PointPosition>> ablepos;

    public List<DunRoom> Rooms = new List<DunRoom>(); 


    public void init()
    {


        dungeon = new GameObject();

        
        ablepos = new Dictionary<Vector2, KeyValuePair<DunRoom, PointPosition>>();
        disablePos = new List<Vector2>();

        Rooms = new List<DunRoom>();

        if (GameObject.Find("dungeon"))
        {
            GameObject go = GameObject.Find("dungeon");
            GameManager.Resource.Destroy(go);
        }
        dungeon = new GameObject();
        dungeon.name = "dungeon";

        if (GameObject.Find("room"))
        {
            GameObject go = GameObject.Find("room");
            GameManager.Resource.Destroy(go);
        }

        roomObj = new GameObject();
        roomObj.name = "room";
        roomObj.transform.SetParent(dungeon.transform);

        if (GameObject.Find("corridor"))
        {
            GameObject go = GameObject.Find("corridor");
            GameManager.Resource.Destroy(go);
        }

        corridorObj = new GameObject();
        corridorObj.name = "corridor";
        corridorObj.transform.SetParent(dungeon.transform);

        Doors = new GameObject();
        Doors.name = "doors";
        Doors.transform.SetParent(dungeon.transform);

        //                  U, R, L, D
        xpos = new int[4] { 0, 1, -1, 0 };      
        ypos = new int[4] { 1, 0, 0, -1 };
    }

    public List<DunRoom> DungeonGen(int roomN)
    {
        init();


        DunRoom room = new DunRoom();
        Rooms.Add(StartRoomGen());

        for (int i = 1; i < roomN; i++)
        {
            room = DungeonRoomGen();
            room.roomID = i;
            Rooms.Add(room);
        }

        bg = new GameObject();
        bg.AddComponent<SpriteRenderer>();
        bg.GetComponent<SpriteRenderer>().sprite = GameManager.Resource.LoadSprite("System/Square");
        bg.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        bg.name = "bg";
        bg.transform.SetParent(dungeon.transform);

        int minx = 10, miny = 10, maxX = 0, maxY = 0;

        foreach(DunRoom rm in Rooms)
        {
            if(minx >= rm.x)
            {
                minx = rm.x;
            }
            if(miny >= rm.y)
            {
                miny = rm.y;
            }
            if(maxX <= rm.x)
            {
                maxX = rm.x;
            }
            if(maxY <= rm.y)
            {
                maxY = rm.y;
            }
        }

        bg.transform.position = new Vector3((minx + maxX) / 2, (miny + maxY) / 2, 5);
        bg.transform.localScale = new Vector3(((maxX - minx) + 30)* 2.56f, ((maxY - miny) + 30) * 2.56f, 1);

        GameManager.Instance.playerGen();

        return Rooms;
    }

    public void test()
    {
        DungeonRoomGen();
        GameObject go = GameObject.Find("a");
        GameManager.Resource.Destroy(go);


    }

    public DunRoom StartRoomGen()
    {
        DunRoom room = GameManager.Resource.Instantiate("Dungeon/Room/StartRoom", roomObj.transform).GetComponent<DunRoom>();
        room.gameObject.transform.SetParent(room.transform);
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
        DunRoom room = GameManager.Resource.Instantiate("Dungeon/Room/Room1", roomObj.transform).GetComponent<DunRoom>();
        room.gameObject.transform.SetParent(room.transform);
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
        sroom.Doors.Add(go);

        GameManager.Resource.Destroy(target.gameObject);
        type = 10 - type;
        
        Tile troomTile = troom.tiles.Find(tile => tile.x == pos.x && tile.y == pos.y);
        if (troomTile != null)
        {
            go = GameManager.Resource.Instantiate(troom.doorTiles[type / 2 - 1], troom.Tiles.transform);
            go.transform.position = troomTile.transform.position;
            go.GetComponent<Tile>().x = (int)pos.x;
            go.GetComponent<Tile>().y = (int)pos.y;
            troom.Doors.Add(go);
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
        go.name = "corridor";
        go.transform.SetParent(corridorObj.transform);
        GameObject obj;
        foreach (KeyValuePair<Vector2, int> tile in corridor)
        {
            switch (tile.Value)
            {
                case 1:
                    obj = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_Column", dungeon.transform);
                    obj.transform.SetParent(go.transform);
                    obj.transform.position = new Vector3(tile.Key.x * 2.56f, tile.Key.y * 2.56f, 2);
                    obj.GetComponent<Tile>().x = (int)Math.Round(tile.Key.x);
                    obj.GetComponent<Tile>().y = (int)Math.Round(tile.Key.y);
                    break;
                case 2:
                    obj = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_LeftDown", dungeon.transform);
                    obj.transform.SetParent(go.transform);
                    obj.transform.position = new Vector3(tile.Key.x * 2.56f, tile.Key.y * 2.56f, 2);
                    obj.GetComponent<Tile>().x = (int)Math.Round(tile.Key.x);
                    obj.GetComponent<Tile>().y = (int)Math.Round(tile.Key.y);
                    break;
                case 3:
                    obj = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_LeftUp", dungeon.transform);
                    obj.transform.SetParent(go.transform);
                    obj.transform.position = new Vector3(tile.Key.x * 2.56f, tile.Key.y * 2.56f, 2);
                    obj.GetComponent<Tile>().x = (int)Math.Round(tile.Key.x);
                    obj.GetComponent<Tile>().y = (int)Math.Round(tile.Key.y);
                    break;
                case 4:
                    obj = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_RightDown", dungeon.transform);
                    obj.transform.SetParent(go.transform);
                    obj.transform.position = new Vector3(tile.Key.x * 2.56f, tile.Key.y * 2.56f, 2);
                    obj.GetComponent<Tile>().x = (int)Math.Round(tile.Key.x);
                    obj.GetComponent<Tile>().y = (int)Math.Round(tile.Key.y);
                    break;
                case 5:
                    obj = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_RightUp", dungeon.transform);
                    obj.transform.SetParent(go.transform);
                    obj.transform.position = new Vector3(tile.Key.x * 2.56f, tile.Key.y * 2.56f, 2);
                    obj.GetComponent<Tile>().x = (int)Math.Round(tile.Key.x);
                    obj.GetComponent<Tile>().y = (int)Math.Round(tile.Key.y);
                    break;
                case 6:
                    obj = GameManager.Resource.Instantiate("Dungeon/Corridor/Corridor_Vertical", dungeon.transform);
                    obj.transform.SetParent(go.transform);
                    obj.transform.position = new Vector3(tile.Key.x * 2.56f, tile.Key.y * 2.56f, 2);
                    obj.GetComponent<Tile>().x = (int)Math.Round(tile.Key.x);
                    obj.GetComponent<Tile>().y = (int)Math.Round(tile.Key.y);
                    break;

            }


            if (ablepos.ContainsKey(pos))
            {
                ablepos.Remove(pos);
            }
            disablePos.Add(pos);

        }
    }

    // When New Dungeon Create or Next Dungeon Create buffer Clear
    public void clear()
    {
        ablepos.Clear();
        disablePos.Clear();
    }

}
