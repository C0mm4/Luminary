using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct SerializedPlayerStatus
{
    public int dexterity; // dex
    public int strength;  // str
    public int Intellect; // int

    public int baseDMG; // base DMG
    public int increaseDMG; // const increase DMG
    public float pIncreaseDMG; // percent increase DMG
    public int finalDMG;  // player damage by status (Excluding buffs and property damage effect increases)

    public int baseHP; // base HP
    public int increseMaxHP; // const HP increase
    public float pIncreaseMaxHP; // HP increase percent
    public int maxHP; // (base HP + const HP increase) * HP increase Percent (Rounds)
    public int currentHP;

    public int baseMana; // base Mana
    public int increaseMaxMana; // const Mana Increase
    public float pIncreaseMaxMana; // Mana Increase percent
    public int maxMana; // (base Mana + const Mana increase) * Mana increase Percent (Rounds)
    public int currentMana;

    public float basespeed; // base Speed
    public float increaseSpeed; // const increase Speed
    public float pIncreaseSpeed; // percent increase Speed ( 10 % + 20 % = 30 % )
    public float speed; // appying speed

    public float pGetDMG; // get DMG percent default = 1(00%)

    public ElementData element; // element debuf status

    public List<Item> items;
    public List<Item> weapons;
    public List<Item> equips;

    public List<Buff> buffs;
    public List<Buff> endbuffs;

    public int level;
}