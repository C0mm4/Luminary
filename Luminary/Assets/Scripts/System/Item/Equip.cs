using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equip : ItemSlot
{

    private int index;

    public override void OnPointerClick(PointerEventData eventData)
    {

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        // 드래그를 끝낼 때 호출되는 함수
        if (GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex != -1)
        {
            if (eventData.pointerEnter != null)
            {
                Equip equip = eventData.pointerEnter.GetComponent<Equip>();
                if (equip != null)
                {
                    if (equip != null && equip != this)
                    {
                        Item itm1 = GameManager.player.GetComponent<Player>().status.equips[index].item;
                        Item itm2 = GameManager.player.GetComponent<Player>().status.equips[equip.index].item;

                        GameManager.player.GetComponent<Player>().status.equips[index].RemoveItem();
                        GameManager.player.GetComponent<Player>().status.equips[equip.index].RemoveItem();

                        GameManager.player.GetComponent<Player>().status.equips[index].AddItem(itm2);
                        GameManager.player.GetComponent<Player>().status.equips[equip.index].AddItem(itm1);
                    }
                }
                else
                {
                    ItemSlot targetSlot = eventData.pointerEnter.GetComponent<ItemSlot>();

                    if (targetSlot != null && targetSlot != this)
                    {
                        GameManager.player.GetComponent<Player>().Unequip(index, item);
                    }
                }

            }
        }
        GameManager.Resource.Destroy(GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem);
        GameManager.Instance.uiManager.invenFrest();
        GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex = -1;
        GameManager.inputManager.isDragging = false;
    }

}
