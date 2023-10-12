using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotBar : MonoBehaviour, IPointerEnterHandler,  IPointerClickHandler, IPointerExitHandler
{
    private Item _item;

    // set Item's Name, selling Gold text
    public Item Item 
    {
        get { return _item; }
        set
        {
            _item = value;
            if(_item != null)
            {
                img.sprite = Item.data.itemImage;
                txt.text = Item.data.itemName;
                img.color = new Color(1, 1, 1, 1);
                if(index < 18)
                {
                    gold.text = Item.data.sellGold + " G";
                    Debug.Log(Item.data.sellGold);
                }
                else
                {
                    gold.text = Item.data.purchaseGold.ToString() + " G";
                }
            }
            else
            {
                img.color = new Color(1, 1, 1, 0);
                txt.text = "";
                gold.text = "";
            }
        }
    }
    public Image selfImg;
    [SerializeField]
    public Image img;
    [SerializeField]
    public TMP_Text txt;
    [SerializeField]
    public TMP_Text gold;
    public int index;
    [SerializeField]
    public BarInven inven;

    public int originSlot = -1;

    // Input Mouse Actions
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inven.currentMenu < inven.slots.Count && inven.currentMenu > 0)
        {
            inven.outHoverHandler(inven.currentMenu);
        }
        else if(inven.currentMenu == 99)
        {
            inven.confirmButton.GetComponent<ConfirmButton>().outHandler();
        }
        if (Item != null)
        {
            inven.hoverHandler(index);

        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item != null)
        {
            inven.clickHandler(index);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inven.outHoverHandler(index);
    }

    // set Image Colors Mouse Hovering
    public void onCursor()
    {
        if(Item != null)
        {
            if (inven.currentMenu != -1 && inven.currentMenu != 99)
            {
                inven.slots[inven.currentMenu].GetComponent<ItemSlotBar>().outCursor();
            }
            inven.currentMenu = index;
            if (inven.selectIndex != index)
            {
                if(index >= 18)
                {
                    if (!inven.npc.takeALook[index - 18])
                    {
                        selfImg.color = new Color(75f / 255f, 75f / 255f, 75f / 255f);
                    }
                    else
                    {
                        selfImg.color = new Color(164f / 255f, 133f / 255f, 133f / 255f, 1);
                    }
                }
                else
                {
                    selfImg.color = new Color(164f / 255f, 133f / 255f, 133f / 255f, 1);
                }
            }

        }
    }

    // set Image Colors Mouse Hovering end
    public void outCursor()
    {
        if(index >= 18)
        {
            if (!inven.npc.takeALook[index - 18])
            {
                selfImg.color = new Color(75f / 255f, 75f / 255f, 75f / 255f);
            }
            else
            {
                if(inven.selectIndex != index)
                {
                    selfImg.color = Color.white;
                }
            }
        }
        else if(inven.selectIndex != index)
        {
            selfImg.color = Color.white;
        }
    }
    // Set Image Colors Selected Slot
    public void Select()
    {
        if(index >= 18)
        {
            if (!inven.npc.takeALook[index - 18])
            {
                selfImg.color = new Color(75f / 255f, 75f / 255f, 75f / 255f);
            }
            else
            {
                selfImg.color = new Color(204f / 255f, 183f / 255f, 183f / 255f);
                inven.selectIndex = index;
            }
        }
        else
        {
            selfImg.color = new Color(204f / 255f, 183f / 255f, 183f / 255f);
            inven.selectIndex = index;
        }
    }

}
