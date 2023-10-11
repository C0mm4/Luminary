using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotBar : MonoBehaviour, IPointerEnterHandler,  IPointerClickHandler, IPointerExitHandler
{
    private Item _item;
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
            }
            else
            {
                img.color = new Color(1, 1, 1, 0);
                txt.text = "";
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
    public EnchantInven inven;

    public int originSlot = -1;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onCursor();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        inven.clickHandler(index);
    }

    public void onCursor()
    {
        inven.currentIndex = index;
        if(inven.selectIndex != index)
        {

            selfImg.color = new Color(164f / 255f, 133f / 255f, 133f / 255f, 1);
        }
    }

    public void outCursor()
    {
        if(inven.selectIndex != index)
        {
            selfImg.color = Color.white;

        }
    }

    public void Select()
    {
        selfImg.color = new Color(204f / 255f, 183f / 255f, 183f / 255f);
        inven.selectIndex = index;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outCursor();
    }
}
