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
