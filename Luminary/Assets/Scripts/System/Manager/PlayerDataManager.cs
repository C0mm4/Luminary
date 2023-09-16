using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static SerializedKeySetting keySetting = new SerializedKeySetting();
    public static SerializedPlayerStatus playerStatus = new SerializedPlayerStatus();

    public static GameObject interactionObject = null;
    public static float interactionDistance;
    public static bool isInteractObjDetect = false;
    public static GameObject interactUI;


    public struct SerializedKeySetting
    {
        public KeyCode inventoryKey;
        public KeyCode InteractionKey;
    }

    public void playerDataInit()
    {
        playerStatus.baseHP = 10;
        playerStatus.baseMana = 100;
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
        playerStatus.element = new ElementData();
        playerStatus.inventory = new List<ItemSlotChara>();
        playerStatus.equips = new List<EquipSlotChara>();
        playerStatus.weapons = new List<WeaponSlotChara>();
        playerStatus.buffs = new List<Buff>();
        playerStatus.endbuffs = new List<Buff>();

        for(int i = 0; i < 12; i++)
        {
            playerStatus.inventory.Add(new ItemSlotChara());
        }
        for(int i = 0; i < 4; i++)
        {
            playerStatus.equips.Add(new EquipSlotChara());
        }
        for(int i = 0; i < 2; i++)
        {
            playerStatus.weapons.Add(new WeaponSlotChara());
        }
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
