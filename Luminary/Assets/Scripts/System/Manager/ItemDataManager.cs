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


    public void Init()
    {
        foreach(ItemData item in data)
        {
            if(item != null)
            {
                itemDictionary[item.itemIndex] = item;
            }
        }
        foreach(ItemData item in enchantData)
        {
            SerializeEnchantData data = new SerializeEnchantData();
            setEnchantData(item, data);
            enchantDictionary.Add(data);
        }
    }

    public ItemData getItemData(int itemIndex)
    {
        ItemData data = ScriptableObject.CreateInstance<ItemData>();
        data.Initialize(itemDictionary[itemIndex]);
        return data;
    }

    public Item ItemGen(int itemindex)
    {
        Item item = new Item();
        item.data = getItemData(itemindex);
        Type T = Type.GetType(item.data.funcName);
        ItemFunc func = Activator.CreateInstance(T) as ItemFunc;
        item.data.func = func;
        item.initCalc();
//        SetEnchantTable(item);
        return item;
    }

    public void setEnchantData(ItemData item, SerializeEnchantData status)
    {
        status.dex = item.baseDex;
        status.strength = item.baseStr;
        status.intellect = item.baseInt;
        status.increaseDMG = item.baseIncDMG;
        status.pincreaseDMG = item.basepIncDMG;
        status.increaseHP = item.baseIncHP;
        status.pincreaseHP = item.basepIncHP;
        status.increaseMP = item.baseIncMP;
        status.pincreaseMP = item.basepIncMP;
        status.increaseSpeed = item.baseIncSpd;
        status.pincreaseSpeed = item.basepIncSpd;

        status.igniteDMG = item.igniteDMG;
        status.freezeDMG = item.freezeDMG;
        status.flowDMG = item.flowDMG;
        status.shockDMG = item.shockDMG;
        status.electDMG = item.electDMG;
        status.seedDMG = item.seedDMG;
        status.meltingDMG = item.meltingDMG;
        status.extinguishDMG = item.extinguishDMG;
        status.fireDMG = item.fireDMG;
        status.electFireDMG = item.electFireDMG;
        status.burnningDMG = item.burnningDMG;
        status.crackedDMG = item.crackedDMG;
        status.rootedDMG = item.rootedDMG;
        status.electShockDMG = item.electShockDMG;
        status.expandDMG = item.expandDMG;
        status.sproutDMG = item.sproutDMG;
        status.dischargeDMG = item.dischargeDMG;
        status.weatheringDMG = item.weatheringDMG;
        status.boostDMG = item.boostDMG;
        status.diffusionDMG = item.diffusionDMG;
        status.overloadDMG = item.overloadDMG;
        status.executionDMG = item.executionDMG;

        status.pGetDMG = item.basepGetDMG;
    }

    public void SetEnchantTable(Item item)
    {
        item.data.increaseStatus = enchantDictionary[item.data.enchantType];
    }
}
