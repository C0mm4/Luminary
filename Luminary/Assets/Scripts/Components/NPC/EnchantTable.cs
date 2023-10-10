using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnchantTable : Menu
{
    public Item targetItem;

    [SerializeField]
    public TMP_Text level;
    public TMP_Text reqGold;
    public TMP_Text curGold;
    public TMP_Text str;
    public TMP_Text dex;
    public TMP_Text intelect;

    public override void InputAction()
    {
        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            
        }
    }

    public void LevelUp()
    {
        targetItem.data.StatusUpgrade();
    }
}
