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
    public string itemName;
    public int itemIndex;



    // Start is called before the first frame update
    public void execute()
    {

    }
}
