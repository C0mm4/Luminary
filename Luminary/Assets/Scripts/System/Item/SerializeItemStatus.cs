using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SerializeItemStatus
{
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

    public static SerializeItemStatus operator + (SerializeItemStatus left, SerializeItemStatus right)
    {
        return new SerializeItemStatus
        {
            dex = left.dex + right.dex,
            strength = left.strength + right.strength,
            intellect = left.intellect + right.intellect,

            increaseDMG = left.increaseDMG + right.increaseDMG,
            pincreaseDMG = left.pincreaseDMG + right.pincreaseDMG,

            increaseHP = left.increaseHP + right.increaseHP,
            pincreaseHP = left.pincreaseHP + right.pincreaseHP,

            increaseMP = left.increaseMP + right.increaseMP,
            pincreaseMP = left.pincreaseMP + right.pincreaseMP,

            increaseSpeed = left.increaseSpeed + right.increaseSpeed,
            pincreaseSpeed = left.pincreaseSpeed + right.pincreaseSpeed,

            igniteDMG = left.igniteDMG + right.igniteDMG,
            freezeDMG = left.freezeDMG + right.freezeDMG,
            flowDMG = left.flowDMG + right.flowDMG,
            shockDMG = left.shockDMG + right.shockDMG,
            electDMG = left.electDMG + right.electDMG,
            seedDMG = left.seedDMG + right.seedDMG,

            meltingDMG = left.meltingDMG + right.meltingDMG,
            extinguishDMG = left.extinguishDMG + right.extinguishDMG,
            fireDMG = left.fireDMG + right.fireDMG,
            electFireDMG = left.electFireDMG + right.electFireDMG,
            burnningDMG = left.burnningDMG + right.burnningDMG,
            crackedDMG = left.crackedDMG + right.crackedDMG,
            rootedDMG = left.rootedDMG + right.rootedDMG,
            electShockDMG = left.electShockDMG + right.electShockDMG,
            expandDMG = left.expandDMG + right.expandDMG,
            sproutDMG = left.sproutDMG + right.sproutDMG,
            dischargeDMG = left.dischargeDMG + right.dischargeDMG,
            weatheringDMG = left.weatheringDMG + right.weatheringDMG,
            boostDMG = left.boostDMG   + right.boostDMG,
            diffusionDMG = left.diffusionDMG + right.diffusionDMG,
            overloadDMG = left.overloadDMG  + right.overloadDMG,
            executionDMG = left.executionDMG + right.executionDMG,

            pGetDMG = left.pGetDMG + right.pGetDMG
        };
    }
}
