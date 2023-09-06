using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotChara 
{
    public Item item = null;

    public void AddItem(Item item)
    {
        this.item = item;
        GameManager.player.GetComponent<Player>().currentweaponSize++;
    }

    public void RemoveItem()
    {
        this.item = null;
        GameManager.player.GetComponent<Player>().currentweaponSize--;
    }
}
