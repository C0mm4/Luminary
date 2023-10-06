using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DropItem : InteractionTrriger
{
    public Item item;


    [SerializeField]
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        interactDist = 2f;
    }

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

    public void setSpr()
    {
        spriteRenderer.sprite = item.data.itemImage;
    }

    public override void isInteraction()
    {
        if (GameManager.player.GetComponent<Charactor>().ItemAdd(item))
        {
            PlayerDataManager.interactionObject = null;

            base.isInteraction();
            GameManager.Resource.Destroy(popupUI);
            GameManager.Resource.Destroy(this.gameObject);
        }
    }
}
