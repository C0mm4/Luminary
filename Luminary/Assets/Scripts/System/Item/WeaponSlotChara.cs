using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotChara 
{
    public Item item = null;
    public Charactor target = null;

    public void AddItem(Item item)
    {
        this.item = item;
        target.currentweaponSize++;
    }

    public void RemoveItem()
    {
        this.item = null;
        target.currentweaponSize--;
    }
}
