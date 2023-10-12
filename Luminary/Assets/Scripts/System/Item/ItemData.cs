using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemData : ScriptableObject, Command
{
    [SerializeField]
    public Sprite itemImage;
    public int type;    // 0 == weapon 1 == passive
    public int enchantType;
    public int level = 0;
    public string itemName;
    public int itemIndex;
    public int spellnum;

    [SerializeField]
    public string funcName;

    public ItemFunc func;

    [SerializeField]
    public int baseDex;
    public int baseInt;
    public int baseStr;

    public int baseIncDMG;
    public int basepIncDMG;

    public int baseIncHP;
    public int basepIncHP;

    public int baseIncMP;
    public int basepIncMP;

    public float baseIncSpd;
    public float basepIncSpd;


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

    public int basepGetDMG;

    public string effectText;

    [SerializeField]
    public SerializeItemStatus status;
    public SerializeEnchantData increaseStatus;

    public void Initialize(ItemData data)
    {
        itemImage = data.itemImage;
        type = data.type;
        enchantType = data.enchantType;
        level = data.level;
        itemName = data.itemName;
        itemIndex = data.itemIndex;
        spellnum = data.spellnum;

        funcName = data.funcName;

        baseDex = data.baseDex;
        baseInt = data.baseInt;
        baseStr = data.baseStr;

        baseIncDMG = data.baseIncDMG;
        basepIncDMG = data.basepIncDMG;

        baseIncHP = data.baseIncHP;
        basepIncHP = data.basepIncHP;

        baseIncMP = data.baseIncMP;
        basepIncMP = data.basepIncMP;

        baseIncSpd = data.baseIncSpd;
        basepIncSpd = data.basepIncSpd;

        igniteDMG = data.igniteDMG;
        freezeDMG = data.freezeDMG;
        flowDMG = data.flowDMG;
        shockDMG = data.shockDMG;
        electDMG = data.electDMG;
        seedDMG = data.seedDMG;

        meltingDMG = data.meltingDMG;
        extinguishDMG = data.extinguishDMG;
        fireDMG = data.fireDMG;
        electFireDMG = data.electFireDMG;
        burnningDMG = data.burnningDMG;
        crackedDMG = data.crackedDMG;
        rootedDMG = data.rootedDMG;
        electShockDMG = data.electShockDMG;
        expandDMG = data.expandDMG;
        sproutDMG = data.sproutDMG;
        dischargeDMG = data.dischargeDMG;
        weatheringDMG = data.weatheringDMG;
        boostDMG = data.boostDMG;
        diffusionDMG = data.diffusionDMG;
        overloadDMG = data.overloadDMG;
        executionDMG = data.executionDMG;

        basepGetDMG = data.basepGetDMG;

        effectText = data.effectText;

        status = new SerializeItemStatus()
        {

            dex = data.baseDex,
            intellect = data.baseInt,
            strength = data.baseStr,

            increaseDMG = data.baseIncDMG,
            pincreaseDMG = data.basepIncDMG,

            increaseHP = data.basepIncHP,
            pincreaseHP = data.basepIncHP,

            increaseMP = data.baseIncMP,
            pincreaseMP = data.basepIncMP,

            increaseSpeed = data.baseIncSpd,
            pincreaseSpeed = data.basepIncSpd,

            igniteDMG = data.igniteDMG,
            freezeDMG = data.freezeDMG,
            flowDMG = data.flowDMG,
            shockDMG = data.shockDMG,
            electDMG = data.electDMG,
            seedDMG = data.seedDMG,

            meltingDMG = data.meltingDMG,
            extinguishDMG = data.extinguishDMG,
            fireDMG = data.fireDMG,
            electFireDMG = data.electFireDMG,
            burnningDMG = data.burnningDMG,
            crackedDMG = data.crackedDMG,
            rootedDMG = data.rootedDMG,
            electShockDMG = data.electShockDMG,
            expandDMG = data.expandDMG,
            sproutDMG = data.sproutDMG,
            dischargeDMG = data.dischargeDMG,
            weatheringDMG = data.weatheringDMG,
            boostDMG = data.boostDMG,
            diffusionDMG = data.diffusionDMG,
            overloadDMG = data.overloadDMG,
            executionDMG = data.executionDMG,

            pGetDMG = data.basepGetDMG,

        };
        increaseStatus = data.increaseStatus;
    }

    public void StatusUpgrade()
    {
        status.dex += increaseStatus.dex;
        status.strength += increaseStatus.strength;
        status.intellect += increaseStatus.intellect;

        status.increaseDMG += increaseStatus.increaseDMG;
        status.pincreaseDMG += increaseStatus.pincreaseDMG;

        status.increaseHP += increaseStatus.increaseHP;
        status.pincreaseHP += increaseStatus.pincreaseHP;

        status.increaseMP += increaseStatus.increaseMP;
        status.pincreaseMP += increaseStatus.pincreaseMP;

        status.increaseSpeed += increaseStatus.increaseSpeed;
        status.pincreaseSpeed += increaseStatus.pincreaseSpeed;

        status.igniteDMG += increaseStatus.igniteDMG;
        status.freezeDMG += increaseStatus.freezeDMG;
        status.flowDMG += increaseStatus.flowDMG;
        status.shockDMG += increaseStatus.shockDMG;
        status.electDMG += increaseStatus.electDMG;
        status.seedDMG += increaseStatus.seedDMG;

        status.meltingDMG += increaseStatus.meltingDMG;
        status.extinguishDMG += increaseStatus.extinguishDMG;
        status.fireDMG += increaseStatus.fireDMG;
        status.electFireDMG += increaseStatus.electFireDMG;
        status.burnningDMG += increaseStatus.burnningDMG;
        status.crackedDMG += increaseStatus.crackedDMG;
        status.rootedDMG += increaseStatus.rootedDMG;
        status.electShockDMG += increaseStatus.electShockDMG;
        status.expandDMG += increaseStatus.expandDMG;
        status.sproutDMG += increaseStatus.sproutDMG;
        status.dischargeDMG += increaseStatus.dischargeDMG;
        status.weatheringDMG += increaseStatus.weatheringDMG;
        status.boostDMG += increaseStatus.boostDMG;
        status.diffusionDMG += increaseStatus.diffusionDMG;
        status.overloadDMG += increaseStatus.overloadDMG;
        status.executionDMG += increaseStatus.executionDMG;

        status.pGetDMG += increaseStatus.pGetDMG;


        level++;
    }

    // Start is called before the first frame update
    public void execute()
    {

    }
}
