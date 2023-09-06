using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotChara
{
    public Item item = null;

    public void AddItem(Item item)
    {
        this.item = item;
        GameManager.player.GetComponent<Player>().currentequipSize++;
    }

    public void RemoveItem()
    {
        this.item = null;
        GameManager.player.GetComponent<Player>().currentequipSize--;
    }
}
