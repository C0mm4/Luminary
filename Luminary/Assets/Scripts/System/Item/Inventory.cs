using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private ItemSlot[] slots;
    
    [SerializeField]
    private Transform bag;

    public List<Item> items;
    
    private void OnValidate()
    {
        slots = bag.GetComponentsInChildren<ItemSlot>();
        GetComponent<RectTransform>().localScale = Vector3.one;
    }
    
    private void Awake()
    {
        freshSlot();
    }

    public void freshSlot()
    {
        int i = 0;
        for (; i < slots.Length && i < items.Count; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void addItem(Item _item)
    {
        if(items.Count < slots.Length)
        {
            items.Add(_item);
            freshSlot();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }
}
