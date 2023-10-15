using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDataManager : MonoBehaviour
{

    [SerializeField]
    public List<ItemData> data = new List<ItemData>();
    public List<ItemData> enchantData = new List<ItemData>();

    public Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();

    public List<SerializeEnchantData> enchantDictionary = new List<SerializeEnchantData>();

    public int commonN, rareN, uniqueN, epicN;


    public void Init()
    {
        commonN = rareN = uniqueN = epicN = 0;
        foreach(ItemData item in data)
        {
            if(item != null)
            {
                itemDictionary[item.itemIndex] = item;
                int rarity = item.itemIndex / 100;
                Debug.Log(rarity);
                switch (rarity)
                {
                    case 100020:
                        commonN++;
                        break;
                    case 100021:
                        rareN++;
                        break;
                    case 100022:
                        uniqueN++;
                        break;
                    case 100023:
                        epicN++;
                        break;
                }
            }
        }
        Debug.Log(commonN); Debug.Log(rareN); Debug.Log(uniqueN); Debug.Log(epicN);
    }

    public ItemData getItemData(int itemIndex)
    {
        ItemData data = ScriptableObject.CreateInstance<ItemData>();
        data.Initialize(itemDictionary[itemIndex]);
        Debug.Log(data.sellGold);
        return data;
    }

    public Item ItemGen(int itemindex)
    {
        Item item = new Item();
        item.data = getItemData(itemindex);
        Type T = Type.GetType(item.data.funcName);
        ItemFunc func = Activator.CreateInstance(T) as ItemFunc;
        item.data.func = func;
        item.data.func.data = item.data;
        item.initCalc();

        return item;
    }

    public Item RandomItemGen()
    {
        int index = 100020;
        int rnd = GameManager.Random.getShopNext();
        int rarity = 0;
        if (rnd < 40)
        {
            rarity = 0;
        }
        else if(rnd < 70)
        {
            rarity = 1;
        }
        else if(rnd < 90)
        {
            rarity = 2;
        }
        else
        {
            rarity = 3;
        }
        index += rarity;
        index *= 100;
        int specificIndex;
        switch (rarity)
        {
            case 0:
                specificIndex = GameManager.Random.getShopNext(1, commonN + 1);
                index += specificIndex;
                break;
            case 1:
                specificIndex = GameManager.Random.getShopNext(1, rareN + 1);
                index += specificIndex;
                break;
            case 2:
                specificIndex = GameManager.Random.getShopNext(1, uniqueN + 1);
                index += specificIndex;
                break;
            case 3:
                specificIndex = GameManager.Random.getShopNext(1, epicN + 1);
                index += specificIndex;
                break;
        }

        Item item = ItemGen(index);


        return item;
    }


}
