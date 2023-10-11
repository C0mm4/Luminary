using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnchant : Choice
{
    public override void Work()
    {
        GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/Shop_Enchant_Inventory");
        Func.SetRectTransform(go);
    }

}
