using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropItem : InteractionTrriger
{
    Item item;

    [SerializeField]
    ItemData data;

    public void Start()
    {
        item = new Item();
        setItemData(data);
    }

    public void setItemData(ItemData data)
    {
        item.data = data;

    }

    public override void isInteraction()
    {
        if (GameManager.player.GetComponent<Charactor>().ItemAdd(item))
        {
            GameManager.Resource.Destroy(this.gameObject);
        }
        
    }
}
