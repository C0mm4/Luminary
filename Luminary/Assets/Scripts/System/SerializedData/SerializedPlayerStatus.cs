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
    public int pIncreaseDMG; // percent increase DMG
    public int finalDMG;  // player damage by status (Excluding buffs and property damage effect increases)

    // base element debuff dmg increase     value(%)
    public int igniteDMG;
    public int freezeDMG;
    public int flowDMG;
    public int shockDMG;
    public int electDMG;
    public int seedDMG;

    // combionate element debuff dmg increase    value(%)
    public int meltingDMG;
    public int extinguishDMG;
    public int fireDMG;
    public int electFireDMG;
    public int burnningDMG;
    public int crackedDMG;
    public int rootedDMG;
    public int electShockDMG;
    public int expandDMG;
    public int sproutDMG;
    public int dischargeDMG;
    public int weatheringDMG;
    public int boostDMG;
    public int diffusionDMG;
    public int overloadDMG;
    public int executionDMG;


    public int def; // deffensive point def % damage decrease

    public int baseHP; // base HP
    public int increseMaxHP; // const HP increase
    public int pIncreaseMaxHP; // HP increase percent
    public int maxHP; // (base HP + const HP increase) * HP increase Percent (Rounds)
    public int currentHP;

    public int baseMana; // base Mana
    public int increaseMaxMana; // const Mana Increase
    public int pIncreaseMaxMana; // Mana Increase percent
    public int maxMana; // (base Mana + const Mana increase) * Mana increase Percent (Rounds)
    public int currentMana;

    public float basespeed; // base Speed
    public float increaseSpeed; // const increase Speed
    public float pIncreaseSpeed; // percent increase Speed ( 10 % + 20 % = 30 % )
    public float speed; // appying speed

    public int pGetDMG; // get DMG percent default = 1(00%)

    public bool godMode; // hitbox doesn't work

    public ElementData element; // element debuf status

    public List<WeaponSlotChara> weapons;
    public List<EquipSlotChara> equips;

    public List<ItemSlotChara> inventory;

    public List<Buff> buffs;
    public List<Buff> endbuffs;

    public int level;
}