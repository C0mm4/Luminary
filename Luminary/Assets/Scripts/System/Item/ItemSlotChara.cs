using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSlotChara
{
    public Item item = null;


    public void AddItem(Item item)
    {
        this.item = item;
        GameManager.player.GetComponent<Charactor>().currentInvenSize++;

    }

    public void RemoveItem() 
    {
        this.item = null;
        GameManager.player.GetComponent<Charactor>().currentInvenSize--;
    }

    
}
