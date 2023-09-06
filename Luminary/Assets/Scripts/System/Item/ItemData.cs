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
    public Item itemfunc;

    // Start is called before the first frame update
    public void execute()
    {

    }
}
