using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotChara
{
    public Item item = null;

    public void AddItem(Item item)
    {
        this.item = item;
        item.data.func.EquipEffect();
        GameManager.player.GetComponent<Player>().currentequipSize++;
    }

    public void RemoveItem()
    {
        item.data.func.UnEquipEffect();
        this.item = null;
        GameManager.player.GetComponent<Player>().currentequipSize--;
    }
}
