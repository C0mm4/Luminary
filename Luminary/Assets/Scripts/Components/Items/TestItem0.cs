using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemFunc
{
    
    public override void EquipEffect()
    {
        Debug.Log("TestItem Equiped");
    }

    public override void OnDamagedEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void OnFrameEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHitEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void UnEquipEffect()
    {
        Debug.Log("TestItem UnEquip");
    }

}
