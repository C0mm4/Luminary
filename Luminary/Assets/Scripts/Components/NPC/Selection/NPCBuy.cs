using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBuy : Choice
{
    public override void Work()
    {
        GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/Shop_Buy");
        go.GetComponent<BarInven>().npc = npc;
        Func.SetRectTransform(go);
    }
}
