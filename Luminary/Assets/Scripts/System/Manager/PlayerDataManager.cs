using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static SerializedKeySetting keySetting = new SerializedKeySetting();
    public struct SerializedKeySetting
    {
        public KeyCode inventoryKey;
        public KeyCode InteractionKey;
    }

    public void initializingKeySetting()
    {
        keySetting.inventoryKey = KeyCode.I;
        keySetting.InteractionKey = KeyCode.G;
    }
    public void loadKeySetting()
    {
        int keyCodeSave = PlayerPrefs.GetInt("KeyCode", 0);
        if(keyCodeSave == 1)
        {
            keySetting.inventoryKey = (KeyCode)PlayerPrefs.GetInt("inventoryKey", 0);
            keySetting.InteractionKey = (KeyCode)PlayerPrefs.GetInt("InteractionKey", 0);
        }
        else
        {
            initializingKeySetting();
        }
    }
    public void saveKeySetting()
    {
        PlayerPrefs.SetInt("inventoryKey", (int)keySetting.inventoryKey);
        PlayerPrefs.SetInt("InteractionKey", (int)keySetting.InteractionKey);
    }



}
