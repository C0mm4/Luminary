using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDataManager : MonoBehaviour
{

    [SerializeField]
    public List<ItemData> data = new List<ItemData>();


    public Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();

    public void Init()
    {
        foreach(ItemData item in data)
        {
            if(item != null)
            {
                itemDictionary[item.itemIndex] = item;
            }
        }
    }

    public ItemData getItemData(int itemIndex)
    {
        return itemDictionary[itemIndex];
    }

    public Item ItemGen(int itemindex)
    {
        Item item = new Item();
        item.data = getItemData(itemindex);
        Type T = Type.GetType(item.data.funcName);
        ItemFunc func = Activator.CreateInstance(T) as ItemFunc;
        item.data.func = func;
        item.initCalc();

        return item;
    }
}
