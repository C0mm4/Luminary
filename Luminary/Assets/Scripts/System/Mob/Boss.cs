using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Mob
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        isboss = true;
        UIGen();
    }

    public void UIGen()
    {
        GameObject go = GameManager.Resource.Instantiate("UI/BossUI/BossUI");
        Func.SetRectTransform(go);
        BossUI ui = go.GetComponent<BossUI>();
        ui.boss = this;
        ui.SetData();
    }
}
