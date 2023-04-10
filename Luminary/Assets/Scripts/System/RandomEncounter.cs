using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomEncounter
{
    // Start is called before the first frame update
    // ���ӿ� ��� �� ���� �õ尪 ����
    public string gameSeed;


    [SerializeField]
    public System.Random mapSeed;
    public System.Random shopSeed;
    public System.Random generalSeed;

    public RandomEncounter(string str = "")
    {
        init(str);
    }

    public void init(string str)
    {
        mapSeed = new System.Random();
        shopSeed = new System.Random();
        generalSeed = new System.Random();
        gameSeed = str;
        if (gameSeed == "")
        {
            gameSeed = setRandomSeed();
        }
        int seedHash = gameSeed.GetHashCode();
        mapSeed = new System.Random(seedHash);
        shopSeed = new System.Random(seedHash);
        generalSeed = new System.Random(seedHash);
        Debug.Log(gameSeed);
        Debug.Log(seedHash);

 
    }


    public void setSeed(string seed)
    {
        init(seed);

    }

    private string setRandomSeed()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[generalSeed.Next(s.Length)]).ToArray());
    }

    public int getMapNext(int m = 0, int M = 100)
    {
        return mapSeed.Next(m, M);

    }
    public int getShopNext(int m = 0, int M = 100)
    {
        return mapSeed.Next(m, M);

    }
    public int getGeneralNext(int m = 0, int M = 100)
    {
        return mapSeed.Next(m, M);

    }
}
