using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using UnityEngine.UI;


public class Item : Command
{
    [SerializeField]
    public ItemData data;

    public void initCalc()
    {
        data.status.dex = data.baseDex;
        data.status.strength = data.baseStr;
        data.status.intellect = data.baseInt;

        data.status.increaseDMG = data.baseIncDMG;
        data.status.pincreaseDMG = data.basepIncDMG;

        data.status.increaseHP = data.baseIncHP;
        data.status.pincreaseHP = data.basepIncHP;

        data.status.increaseMP = data.baseIncMP;
        data.status.pincreaseMP = data.basepIncMP;

        data.status.increaseSpeed = data.baseIncSpd;
        data.status.pincreaseSpeed = data.basepIncSpd;

        data.status.igniteDMG = data.igniteDMG;
        data.status.freezeDMG = data.freezeDMG;
        data.status.flowDMG = data.flowDMG;
        data.status.shockDMG = data.shockDMG;
        data.status.electDMG = data.electDMG;
        data.status.seedDMG = data.seedDMG;

        data.status.meltingDMG = data.meltingDMG;
        data.status.extinguishDMG = data.extinguishDMG;
        data.status.fireDMG = data.fireDMG;
        data.status.electFireDMG = data.electFireDMG;
        data.status.burnningDMG = data.burnningDMG;
        data.status.crackedDMG = data.crackedDMG;
        data.status.rootedDMG = data.rootedDMG;
        data.status.electShockDMG = data.electShockDMG;
        data.status.expandDMG = data.expandDMG;
        data.status.sproutDMG = data.sproutDMG;
        data.status.dischargeDMG = data.dischargeDMG;
        data.status.weatheringDMG = data.weatheringDMG;
        data.status.boostDMG = data.boostDMG;
        data.status.diffusionDMG = data.diffusionDMG;
        data.status.overloadDMG = data.overloadDMG;
        data.status.executionDMG = data.executionDMG;

        data.status.pGetDMG = data.basepGetDMG;
    }

    // Start is called before the first frame update
    public void execute()
    {
        
    }
}
