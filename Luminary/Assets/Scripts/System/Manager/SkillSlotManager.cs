using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotManager
{

    public SkillSlot[] slots;
    public GameObject slot;
    public SkillSlotUI skillslotUI;

    public void init()
    {
        slot = GameManager.Resource.Instantiate("SkillSlots");
        slots = slot.GetComponentsInChildren<SkillSlot>();
        skillslotUI = slot.GetComponent<SkillSlotUI>();
        SetRoll();
        
    }

    public SkillSlot getSlot(int n)
    {
        return slots[n];
    }

    public void setSlot(int n, Spell cmd)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].getSpell() != null)
            {
                if (slots[i].getSpell().GetHashCode() == cmd.GetHashCode())
                {
                    deSetSlot(i);
                    break;
                }
            }
        }
        slots[n].setCommand(cmd);
        setImg();
    }

    public void setImg()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotImg.updateImg();
        }
    }

    public void deSetSlot(int n)
    {
        slots[n].deSetCommand();
    }


    // test
    public void SetRoll()
    {
        GameObject obj = GameManager.Spells.allspells[0];
        setSlot(0, obj.GetComponent<Spell>());
    }
}
