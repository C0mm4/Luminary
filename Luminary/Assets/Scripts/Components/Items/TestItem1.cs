using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem1 : ItemFunc
{

    public override void EquipEffect()
    {
        base.EquipEffect();
        Debug.Log("TestItem1 Equiped");
    }

    public override void UnEquipEffect()
    {
        base.UnEquipEffect();
        Debug.Log("TestItem1 UnEquip");
    }
}
