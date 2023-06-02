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

    public void playerDataInit()
    {
        playerStatus.baseHP = 10;
        playerStatus.baseDMG = 1;
        playerStatus.basespeed = 5;
        playerStatus.increaseDMG = 0;
        playerStatus.increaseSpeed = 0;
        playerStatus.increseMaxHP = 0;
        playerStatus.pIncreaseDMG = 0;
        playerStatus.pIncreaseMaxHP = 0;
        playerStatus.pIncreaseSpeed = 0;
        playerStatus.pGetDMG = 1;
        playerStatus.level = 1;
    }

    public void loadKeySetting()
    {
        keySetting.inventoryKey = (KeyCode)PlayerPrefs.GetInt("inventoryKey", (int)KeyCode.I);
        keySetting.InteractionKey = (KeyCode)PlayerPrefs.GetInt("InteractionKey", (int)KeyCode.F);
    }
    public void saveKeySetting()
    {
        PlayerPrefs.SetInt("inventoryKey", (int)keySetting.inventoryKey);
        PlayerPrefs.SetInt("InteractionKey", (int)keySetting.InteractionKey);
    }

    public void savePlayerData()
    {

    }

}
