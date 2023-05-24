using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Charactor : MonoBehaviour
{
    // Charactor Base Status
    protected int MaxHP;
    public int HPUp;
    [SerializeField]
    protected int CurrentHP;
    [SerializeField]
    public float speed;
    public float speedIncrease;

    protected int Level;

    // Charactor Buffs / Debufs
    [SerializeField]
    public List<Buff> buffs;
    public List<Buff> endBuffs;

    // Charactor Items
    public List<Item> items;
    public List<Item> equips;

    // Charactor State Machine
    protected StateMachine sMachine;

    // player instance
    public Charactor player;

    // Element State Data
    ElementData element;


    // Start is called before the first frame update
    public virtual void Start()
    {
        buffs = new List<Buff>();
        endBuffs = new List<Buff> ();
        items = new List<Item>();
        equips = new List<Item>();
        sMachine = new StateMachine();
        element = new ElementData();

        GameObject test = GameManager.Resource.Instantiate("Item/Item0");
        test.layer = LayerMask.NameToLayer("Inventory");
        ItemAdd(test.GetComponent<Item>());
    }

    // Update is called once per frame
    public virtual void Update()
    {
        sMachine.updateState();
        runBufss();
    }

    public void runBufss()
    {
        foreach (Buff buff in buffs)
        {
            Debug.Log(buff);
            if (buff != null)
            {
                buff.durateEffect();
            }
        }

        desetBuffs();
    }

    public void desetBuffs()
    {
        foreach (Buff buff in endBuffs)
        {
            if (buff != null)
            {
                buff.endEffect();
            }

        }
        endBuffs.Clear();
    }

    public void HPIncrease(int pts)
    {
        CurrentHP += pts;
        if(CurrentHP <= MaxHP + HPUp)
        {
            CurrentHP = MaxHP + HPUp;
        }
    }

    public void HPDecrease(int pts)
    { 
        CurrentHP -= pts;
        if(CurrentHP <= 0)
        {
            DieObject();
        }
    }

    public virtual void DieObject()
    {
        
        GameManager.Resource.Destroy(this.gameObject);
    }

    public void ItemAdd(Item item)
    {
        if (items.Count < 8)
        {
            items.Add(item);
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }

    public void ItemEqip(Item item) 
    {
        if (equips.Count < 4)
        {
            equips.Add(item);
            items.Remove(item);
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Full Equiped");
        }
    }

    public void ItemUnequip(Item item)
    {
        if (items.Count < 8)
        {
            equips.Remove(item);
            items.Add(item);
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }
}
