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
    public float basepIncHP;

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

    public float basepGetDMG;



    [SerializeField]
    public SerializeItemStatus status;
    public SerializeItemStatus increaseStatus;

    public void StatusUpgrade()
    {
        status += increaseStatus;
    }

    // Start is called before the first frame update
    public void execute()
    {

    }
}
