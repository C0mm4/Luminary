using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEquip : ItemFunc
{

    public override void EquipEffect()
    {
//        GameManager.player.GetComponent<Player>().ItemStatusSum(data.status);
        Debug.Log("TEST");      
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
        Debug.Log("TestItem1 UnEquip");
//        GameManager.player.GetComponent<Player>().ItemStatusminus(data.status);
    }
}
