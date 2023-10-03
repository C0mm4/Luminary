using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotChara 
{
    public Item item = null;

    public void AddItem(Item item)
    {
        this.item = item;
        Debug.Log(item.data.itemName);
        item.data.func.EquipEffect();
        GameManager.player.GetComponent<Player>().currentweaponSize++;
        GameManager.Instance.uiManager.invenFresh();
    }

    public void RemoveItem()
    {
        item.data.func.UnEquipEffect();
        this.item = null;
        GameManager.player.GetComponent<Player>().currentweaponSize--;
    }
}
