using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : NPC
{
    public void Start()
    {
        interactDist = 2f;
        text = "대화한다";
        for(int i = 0; i < 6; i++)
        {
            Item itm = GameManager.itemDataManager.RandomItemGen();
            items.Add(itm);
            takeALook.Add(true);
        }
    }
}
