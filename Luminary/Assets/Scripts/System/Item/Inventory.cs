using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private ItemSlot[] slots;

    [SerializeField]
    private Equip[] equips;
    
    [SerializeField]
    private Transform bag;

    [SerializeField]
    private Transform equip;

    RectTransform rt;

    public GameObject itemin;

    [SerializeField]
    private GameObject target;

    private void OnValidate()
    {
        slots = bag.GetComponentsInChildren<ItemSlot>();
        equips = equip.GetComponentsInChildren<Equip>();
        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void init()
    {
        Debug.Log("Inventory Init");
        rt = GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = Vector3.zero;
        itemin = new GameObject("Item In");
        itemin.transform.parent = this.transform;
        target = GameManager.player;
        // Test
        //GameObject go = GameManager.Resource.Instantiate("Item/Item0");
        //go.transform.parent = itemin.transform;
        //addItem(go);

    }
    
    private void Awake()
    {
        freshSlot();
    }

    public void freshSlot()
    {
        if (target != null)
        {
            int i = 0;
            for (; i < slots.Length && i < target.GetComponent<Charactor>().status.items.Count; i++)
            {
                slots[i].item = target.GetComponent<Charactor>().status.items[i];
            }
            for (; i < equips.Length && i < target.GetComponent<Charactor>().status.equips.Count; i++)
            {
                equips[i].item = target.GetComponent<Charactor>().status.equips[i];
            }
            for (; i < slots.Length; i++)
            {
                slots[i].item = null;
            }
            for (; i < equips.Length; i++)
            {
                equips[i].item = null;
            }
        }
    }

    public void ItemGen()
    {
        int n = Random.Range(0, 9);
        Debug.Log(n);
        GameObject item = GameManager.Resource.Instantiate("Item/Item" + n);
    }

    public void delItem(GameObject _item)
    {
        Item itm = _item.GetComponent<Item>();
        itm.gameObject.layer = 0;
        target.GetComponent<Charactor>().status.items.Remove(itm);
        freshSlot();
    }
}
