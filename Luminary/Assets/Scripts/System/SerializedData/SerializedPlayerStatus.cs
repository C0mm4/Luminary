using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct SerializedPlayerStatus
{
    private int dexterity; // dex
    private int strength;  // str
    private int Intellect; // int

    public int baseDMG; // base DMG
    public int increaseDMG; // const increase DMG
    public float pIncreaseDMG; // percent increase DMG
    public int finalDMG;  // player damage by status (Excluding buffs and property damage effect increases)

    public int baseHP; // base HP
    public int increseMaxHP; // const HP increase
    public float pIncreaseMaxHP; // HP increase percent
    public int maxHP; // (base HP + const HP increase) * HP increase Percent (Rounds)
    public int currentHP;

    public int basespeed; // base Speed
    public int increaseSpeed; // const increase Speed
    public float pIncreaseSpeed; // percent increase Speed ( 10 % + 20 % = 30 % )
    public int speed; // appying speed

    public float pGetDMG; // get DMG percent default = 1(00%)

    public ElementData element; // element debuf status

    public List<Item> items;
    public List<Item> equips;

    public List<Buff> buffs;
    public List<Buff> endbuffs;

    public int level;
}