using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLevelUp : Choice
{
    public override void Work()
    {
        GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/LevelUPUI");
        Func.SetRectTransform(go);
    }
}
