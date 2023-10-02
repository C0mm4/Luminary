using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropItem : InteractionTrriger
{
    Item item;


    [SerializeField]
    SpriteRenderer spriteRenderer;

    public void setItemData(ItemData data)
    {
        item = new Item();
        item.data = data;
        Type T = Type.GetType(data.funcName);
        ItemFunc func = Activator.CreateInstance(T) as ItemFunc;
        item.data.func = func;

        spriteRenderer.sprite = data.itemImage;

        Debug.Log(data.itemName);
    }

    public override void isInteraction()
    {
        if (GameManager.player.GetComponent<Charactor>().ItemAdd(item))
        {
            PlayerDataManager.interactionObject = null;
            GameManager.Resource.Destroy(this.gameObject);
        }
    }
}
