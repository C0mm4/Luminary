using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotManager
{

    public GameObject[] slots;
    public GameObject spells;

    public void init()
    {
        slots = new GameObject[5];
        spells = new GameObject();
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject go = GameManager.Resource.Instantiate("SkillSlot");
            slots[i] = go;

        }
        SetRoll();
    }

    public GameObject getSlot(int n)
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
