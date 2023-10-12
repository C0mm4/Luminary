using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarInven : Menu
{
    public override void ConfirmAction()
    {

    }

    [SerializeField]
    public List<GameObject> slots;


    public Item item;

    [SerializeField]

    public Player player;

    public GameObject confirmButton;

    public int tmpIndex = 0;
    public int selectIndex = 0;
    public override void InputAction()
    {
    }

    // Start is called before the first frame update
    public override void Start()
    {
        selectIndex = -1;
        base.Start();
        player = GameManager.player.GetComponent<Player>();
        invenFresh();
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<ItemSlotBar>().index = i;
            slots[i].GetComponent<ItemSlotBar>().inven = this;
        }

    }

    public void invenFresh()
    {
        // Set Inventory Size
        menusize = player.currentweaponSize + player.currentequipSize + player.currentInvenSize;

        int i = 0;
        for (int j = 0; j < player.status.weapons.Count; j++)
        {
            if (player.status.weapons[j].item != null)
            {
                slots[i].GetComponent<ItemSlotBar>().Item = player.status.weapons[j].item;
                slots[i].GetComponent<ItemSlotBar>().originSlot = j;

                i++;
            }
        }
        for (int j = 0; j < player.status.equips.Count; j++)
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
        for (; i < slots.Count; i++)
        {
            slots[i].GetComponent<ItemSlotBar>().Item = null;
        }
    }
    public virtual void clickHandler(int index)
    {

    }

    public virtual void hoverHandler(int index)
    {

    }

    public virtual void outHoverHandler(int index)
    {

    }
}
