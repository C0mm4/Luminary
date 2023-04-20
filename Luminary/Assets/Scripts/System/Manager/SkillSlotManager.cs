using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotManager
{

    public SkillSlot[] slots;
    public GameObject slot;
    public GameObject spells;
    public SkillSlotUI skillslotUI;

    public void init()
    {
        slot = GameManager.Resource.Instantiate("SkillSlots");
        slots = slot.GetComponentsInChildren<SkillSlot>();
        skillslotUI = slot.GetComponent<SkillSlotUI>();
        spells = new GameObject();
        SetRoll();
    }

    public SkillSlot getSlot(int n)
    {
        return slots[n];
    }

    public void setSlot(int n, Spell cmd)
    {
        slots[n].GetComponent<SkillSlot>().setCommand(cmd);
    }

    public void deSetSlot(int n)
    {
        slots[n].GetComponent<SkillSlot>().deSetCommand();
    }


    // test
    public void SetRoll()
    {
        spells.AddComponent<TestSpell>();
        Spell spl = spells.GetComponent<TestSpell>();
        setSlot(0, spl);
    }
}
