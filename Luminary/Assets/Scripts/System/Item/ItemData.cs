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
