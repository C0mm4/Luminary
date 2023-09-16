using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotChara 
{
    public Item item = null;

    public void AddItem(Item item)
    {
        this.item = item;
        item.data.func.EquipEffect();
        GameManager.player.GetComponent<Player>().currentweaponSize++;
        GameManager.Instance.uiManager.invenFrest();
    }

    public void RemoveItem()
    {
        item.data.func.UnEquipEffect();
        this.item = null;
        GameManager.player.GetComponent<Player>().currentweaponSize--;
    }
}
