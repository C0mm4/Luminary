using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropItem : InteractionTrriger
{
    Item item;

    [SerializeField]
    ItemData data;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        item = new Item();

        // For Test. After, when Item Generate called it
        setItemData(data);
    }

    public void setItemData(ItemData data)
    {
        item.data = data;
        Type T = Type.GetType(data.funcName);
        ItemFunc func = Activator.CreateInstance(T) as ItemFunc;
        data.func = func;
        Debug.Log(data.func.GetType().ToString());

        spriteRenderer.sprite = data.itemImage;
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
