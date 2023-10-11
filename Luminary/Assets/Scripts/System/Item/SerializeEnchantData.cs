using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SerializeEnchantData : ScriptableObject
{
    public int baseGold;
    public int increaseGold;

    public int dex;
    public int strength;
    public int intellect;

    public int increaseDMG;
    public int pincreaseDMG;

    public int increaseHP;
    public int pincreaseHP;

    public int increaseMP;
    public int pincreaseMP;

    public float increaseSpeed;
    public float pincreaseSpeed;

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

    public int pGetDMG;

}
