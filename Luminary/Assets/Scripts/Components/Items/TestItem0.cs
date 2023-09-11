using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemFunc
{
    
    public override void EquipEffect()
    {
        base.EquipEffect();
        Debug.Log("TestItem Equiped");
    }

    public override void UnEquipEffect()
    {
        base.UnEquipEffect();
        Debug.Log("TestItem UnEquip");
    }
}
