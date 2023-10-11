using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantInven : Menu
{
    [SerializeField]
    public List<GameObject> slots;

    [SerializeField]
    public GameObject statusUI;

    public Item item;

    public Player player;

    public int currentIndex = 0;
    public int tmpIndex = 0;
    public int selectIndex = -1;
    public override void InputAction()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentIndex == -1 || currentIndex == 99)
            {
                currentIndex = tmpIndex;
                slots[currentIndex].GetComponent<ItemSlotBar>().onCursor();
            }
            else
            {
                slots[currentIndex].GetComponent<ItemSlotBar>().outCursor();
                currentIndex++;
                if (currentIndex >= menusize)
                {
                    currentIndex = 0;
                }
                slots[currentIndex].GetComponent<ItemSlotBar>().onCursor();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentIndex == -1 || currentIndex == 99)
            {
                currentIndex = tmpIndex;
                slots[currentIndex].GetComponent<ItemSlotBar>().onCursor();
            }
            else
            {
                slots[currentIndex].GetComponent<ItemSlotBar>().outCursor();
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = menusize - 1;
                }
                slots[currentIndex].GetComponent<ItemSlotBar>().onCursor();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentIndex != 99)
            {
                if(currentIndex == -1)
                {
                    currentIndex = tmpIndex;
                    slots[currentIndex].GetComponent<ItemSlotBar>().onCursor();
                }
                else
                {
                    slots[currentIndex].GetComponent<ItemSlotBar>().outCursor();
                    tmpIndex = currentIndex;
                    currentIndex = 99;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentIndex != -1)
            {
                if(currentIndex == 99)
                {
                    currentIndex = tmpIndex;
                    slots[currentIndex].GetComponent<ItemSlotBar>().onCursor();
                }
                else
                {
                    slots[currentIndex].GetComponent<ItemSlotBar>().outCursor();
                    tmpIndex = currentIndex;
                    currentIndex = -1;
                }
            }
        }
        if(Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            if (currentIndex < menusize && currentIndex != -1)
            {
                clickHandler(currentIndex);
            }
            else if (currentIndex == -1)
            {
                GameManager.Instance.uiManager.endMenu();
            }
            else if (currentIndex == 99)
            {
                MoveEnchantTable();
            }
        }
        Debug.Log(currentIndex);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        player = GameManager.player.GetComponent<Player>();
        selectIndex = -1;
        invenFresh();
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<ItemSlotBar>().index = i;
            slots[i].GetComponent<ItemSlotBar>().inven = this;
        }
        slots[currentIndex].GetComponent<ItemSlotBar>().onCursor();
    }

    public void invenFresh()
    {
        menusize = player.currentweaponSize + player.currentequipSize + player.currentInvenSize;
        for (int i = 0; i < slots.Count; i++)
        {
            for (int j = 0; j < player.currentweaponSize; j++)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = player.status.weapons[j].item;
                slots[i].GetComponent<ItemSlotBar>().originSlot = j;
                i++;
            }
            for(int j = 0; j < player.currentequipSize; j++)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = player.status.equips[j].item;
                slots[i].GetComponent<ItemSlotBar>().originSlot = j + 3;
                i++;
            }
            for(int j = 0; j < player.currentInvenSize; j++)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = player.status.inventory[j].item;
                slots[i].GetComponent<ItemSlotBar>().originSlot = j + 7;
                i++;
            }
            for(;i < slots.Count; i++)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = null;
            }
        }
    }

    public void clickHandler(int index)
    {
        int tmp = selectIndex;
        selectIndex = index;
        if(tmp >= 0 && tmp < menusize)
        {
            slots[tmp].GetComponent<ItemSlotBar>().outCursor();

        }
        slots[selectIndex].GetComponent<ItemSlotBar>().Select();
    }

    public void MoveEnchantTable()
    {
        if (slots[selectIndex].GetComponent<ItemSlotBar>().Item.data.type == 0)
        {
            GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/WeaponEnchant");
            go.GetComponent<EnchantTable>().targetItem = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
            go.GetComponent<EnchantTable>().originSlotindex = slots[selectIndex].GetComponent<ItemSlotBar>().originSlot;
            Debug.Log(slots[selectIndex].GetComponent<ItemSlotBar>().Item.data.itemName);
            Debug.Log(slots[selectIndex].GetComponent<ItemSlotBar>().originSlot);
        }
        else
        {
            GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/ItemEnchant");
            go.GetComponent<EnchantTable>().targetItem = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
            go.GetComponent<EnchantTable>().originSlotindex = slots[selectIndex].GetComponent<ItemSlotBar>().originSlot;
        }
    }
}
