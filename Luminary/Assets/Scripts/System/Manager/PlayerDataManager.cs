using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static SerializedKeySetting keySetting = new SerializedKeySetting();
    public static SerializedPlayerStatus playerStatus = new SerializedPlayerStatus();

    public static string interactionObject = null;
    public static float interactionDistance;

    public struct SerializedKeySetting
    {
        public KeyCode inventoryKey;
        public KeyCode InteractionKey;
    }

    public struct SerializedPlayerStatus
    {
        private int dexterity; // dex
        private int strength;  // str
        private int Intellect; // int
        public int playerDamageStatus;  // player damage by status (Excluding buffs and property damage effect increases)
        public int level;
    }

    public void loadKeySetting()
    {
        keySetting.inventoryKey = (KeyCode)PlayerPrefs.GetInt("inventoryKey", (int)KeyCode.I);
        keySetting.InteractionKey = (KeyCode)PlayerPrefs.GetInt("InteractionKey", (int)KeyCode.G);
    }
    public void saveKeySetting()
    {
        PlayerPrefs.SetInt("inventoryKey", (int)keySetting.inventoryKey);
        PlayerPrefs.SetInt("InteractionKey", (int)keySetting.InteractionKey);
    }



}
