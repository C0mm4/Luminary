using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnchantInven : Menu
{
    [SerializeField]
    public List<GameObject> slots;


    public Item item;

    [SerializeField]
    public GameObject targetName;
    public List<GameObject> statusText;
    public List<KeyValuePair<string, int>> status = new List<KeyValuePair<string, int>>();
    public string effectText;
    public Player player;

    public GameObject confirmButton;

    public int tmpIndex = 0;
    public int selectIndex = 0;
    public override void InputAction()
    {
        // Move Current Menu with Arrow Key and wasd
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentMenu == -1 || currentMenu == 99)
            {
                currentMenu = tmpIndex;
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
            else
            {
                slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                currentMenu++;
                if (currentMenu >= menusize)
                {
                    currentMenu = 0;
                }
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentMenu == -1 || currentMenu == 99)
            {
                currentMenu = tmpIndex;
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
            else
            {
                slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                currentMenu--;
                if (currentMenu < 0)
                {
                    currentMenu = menusize - 1;
                }
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentMenu != 99)
            {
                if(currentMenu == -1)
                {
                    currentMenu = tmpIndex;
                    slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
                }
                else
                {
                    slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                    tmpIndex = currentMenu;
                    currentMenu = 99;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentMenu != -1)
            {
                if(currentMenu == 99)
                {
                    currentMenu = tmpIndex;
                    slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
                }
                else
                {
                    slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                    tmpIndex = currentMenu;
                    currentMenu = -1;
                }
            }
        }
        // If current Index is ItemSlot, ItemSlot Select
        if(Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            if (currentMenu < menusize && currentMenu != -1)
            {
                clickHandler(currentMenu);
            }
            // if current Index is close, close menu
            else if (currentMenu == -1)
            {
                GameManager.Instance.uiManager.endMenu();
            }
            // if current slot is Enchant Item, Enchant Table Menu Create
            else if (currentMenu == 99)
            {
                ConfirmAction();
            }
        }
        Debug.Log(currentMenu);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        selectIndex = -1;
        base.Start();
        player = GameManager.player.GetComponent<Player>();
        invenFresh();
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<ItemSlotBar>().index = i;
            slots[i].GetComponent<ItemSlotBar>().inven = this;
        }

    }

    public override void show()
    {
        base.show();
        if(selectIndex != -1)
        {
            StatusUISet();
        }
    }

    public void invenFresh()
    {
        // Set Inventory Size
        menusize = player.currentweaponSize + player.currentequipSize + player.currentInvenSize;

        int i = 0;
        for(int j = 0; j < player.status.weapons.Count; j++)
        {
            if (player.status.weapons[j].item != null)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = player.status.weapons[j].item;
                slots[i].GetComponent<ItemSlotBar>().originSlot = j;
                
                i++;
            }
        }
        for(int j = 0; j < player.status.equips.Count; j++)
        {
            if (player.status.equips[j].item != null)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = player.status.equips[j].item;
                slots[i].GetComponent<ItemSlotBar>().originSlot = j + 3;
                i++;
            }
        }
        for (int j = 0; j < player.status.inventory.Count; j++)
        {
            if (player.status.inventory[j].item != null)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = player.status.inventory[j].item;
                slots[i].GetComponent<ItemSlotBar>().originSlot = j + 7;
                i++;
            }
        }
        for(; i < slots.Count; i++)
        {
            slots[i].GetComponent<ItemSlotBar>().Item = null;
        }
    }
    // When clicked Item Slots, past select slot run outCursor, new Slot set
    
    public void clickHandler(int index)
    {
        int tmp = selectIndex;
        selectIndex = index;
        if(tmp >= 0 && tmp < menusize)
        {
            slots[tmp].GetComponent<ItemSlotBar>().outCursor();

        }
        slots[selectIndex].GetComponent<ItemSlotBar>().Select();

        // Set ConfirmButton Able
        confirmButton.GetComponent<ConfirmButton>().setAble();

        StatusUISet();

    }

    public void StatusUISet()
    {
        // Item Status UI Data Set
        item = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
        targetName.GetComponent<TMP_Text>().text = item.data.itemName;
        status = FindStatus(slots[selectIndex].GetComponent<ItemSlotBar>().Item);
        int i = 0;
        int last = -1;
        for (; i < statusText.Count; i++)
        {
            try
            {
                statusText[i].GetComponent<TMP_Text>().text = status[i].Key + " +" + status[i].Value;

            }
            catch
            {
                statusText[i].GetComponent<TMP_Text>().text = "";
                if (last == -1)
                {
                    last = i;
                }
            }
        }
        statusText[last].GetComponent<TMP_Text>().text = effectText;
    }

    public List<KeyValuePair<string, int>> FindStatus(Item itm)
    {
        // Find Item base Status - str, dex, int, hp, mp - and return
        List<KeyValuePair<string, int>> keyValuePairs = new List<KeyValuePair<string, int>>();
        if (itm.data.status.strength != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("STR", item.data.status.strength);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.dex != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("DEX", item.data.status.dex);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.intellect != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("INT", item.data.status.intellect);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.increaseHP != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("MAX HP", item.data.status.increaseHP);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.increaseMP != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("MAX MP", item.data.status.increaseMP);
            keyValuePairs.Add(data);
        }
        if (itm.data.effectText != "")
        {
            effectText = itm.data.effectText;
        }
        return keyValuePairs;
    }

    public override void ConfirmAction()
    {
        // if Item is Weapon > Weapon Enchant Table
        if(selectIndex != -1 && selectIndex != 99)
        {
            if (slots[selectIndex].GetComponent<ItemSlotBar>().Item.data.type == 0)
            {
                GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/WeaponEnchant");
                go.GetComponent<EnchantTable>().targetItem = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
                go.GetComponent<EnchantTable>().originSlotindex = slots[selectIndex].GetComponent<ItemSlotBar>().originSlot;
                go.GetComponent<EnchantTable>().setData();
            }
            // if Item is not Weapon >> Item Enchant Table
            else
            {
                GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/ItemEnchant");
                go.GetComponent<EnchantTable>().targetItem = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
                go.GetComponent<EnchantTable>().originSlotindex = slots[selectIndex].GetComponent<ItemSlotBar>().originSlot;
                go.GetComponent<EnchantTable>().setData();
            }
        }
    }
}
