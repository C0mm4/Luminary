using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Menu
{
    [SerializeField]
    private ItemSlot[] slots;

    [SerializeField]
    private ItemSlot[] equips;

    [SerializeField]
    private ItemSlot[] equipWeapons;

    [SerializeField]
    private Transform bag;

    [SerializeField]
    private Transform equip;

    RectTransform rt;


    [SerializeField]
    private GameObject target;

    public GameObject tmpitem;

    public int clickIndex = -1;

    public override void Start()
    {

    }

    private void OnValidate()
    {
        slots = bag.GetComponentsInChildren<ItemSlot>();
        equips = equip.GetComponentsInChildren<ItemSlot>();
        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public override void show()
    {
        freshSlot();
        base.show();
    }

    

    public void init()
    {
        Debug.Log("Inventory Init");
        rt = GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = Vector3.zero;
        target = GameManager.player;
        gameObject.SetActive(false);
        // Test
    }

    public void targetSet()
    {
        target = GameManager.player;
        Debug.Log(target);
    }
    
    private void Awake()
    {
        freshSlot();
    }

    public void freshSlot()
    {
        if (target != null)
        {
            for (int i = 0; i < slots.Length && i < target.GetComponent<Charactor>().status.inventory.Count; i++)
            {
                slots[i].item = target.GetComponent<Charactor>().status.inventory[i].item;
            }
            for (int i = 0; i < equips.Length && i < target.GetComponent<Charactor>().status.equips.Count; i++)
            {
                equips[i].item = target.GetComponent<Charactor>().status.equips[i].item;
            }
            for (int i = 0; i < equipWeapons.Length && i < target.GetComponent<Charactor>().status.weapons.Count; i++)
            {
                equipWeapons[i].item = target.GetComponent<Charactor>().status.weapons[i].item;
            }
        }
    }

    public override void InputAction()
    {
        Debug.Log("inventoryInputAction");

        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log("I Key Input");
            GameManager.Instance.uiManager.endMenu();
        }
    }
}
