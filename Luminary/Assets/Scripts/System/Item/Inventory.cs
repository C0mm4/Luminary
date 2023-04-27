using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private ItemSlot[] slots;
    
    [SerializeField]
    private Transform bag;

    public List<Item> items;
    RectTransform rt;

    public GameObject itemin;

    private void OnValidate()
    {
        slots = bag.GetComponentsInChildren<ItemSlot>();
        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void init()
    {
        rt = GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = Vector3.zero;
        itemin = new GameObject("Item In");
        itemin.transform.parent = this.transform;

        // Test
        GameObject go = GameManager.Resource.Instantiate("Item/TestItem");
        go.transform.parent = itemin.transform;
        addItem(go);

    }
    
    private void Awake()
    {
        freshSlot();
    }

    public void freshSlot()
    {
        int i = 0;
        for (; i < slots.Length && i < items.Count; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void addItem(GameObject _item)
    {
        Item itm = _item.GetComponent<Item>();
        if(items.Count < slots.Length)
        {
            _item.transform.parent = itemin.transform;
            items.Add(itm);
            freshSlot();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }
}
