using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
                Debug.Log("Item Exists");
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                Debug.Log("Item doesn't Exists");
            }
        }
    }

}