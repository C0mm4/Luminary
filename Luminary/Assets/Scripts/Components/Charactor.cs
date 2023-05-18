using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    // Charactor Base Status
    protected int MaxHP;
    public int HPUp;
    protected int CurrentHP;
    [SerializeField]
    public float speed;
    public float speedIncrease;

    protected int Level;

    // Charactor Buffs / Debufs
    public List<Buff> buffs;
    public List<Buff> endBuffs;

    // Charactor Items
    public List<Item> items;

    // Charactor State Machine
    protected StateMachine sMachine;

    // player instance
    public Charactor player;


    // Start is called before the first frame update
    public virtual void Start()
    {
        buffs = new List<Buff>();
        endBuffs = new List<Buff> ();
        items = new List<Item>();
        sMachine = new StateMachine();
        GameObject test = GameManager.Resource.Instantiate("Item/Item0");
        //test.layer = LayerMask.NameToLayer("inventory");
        ItemAdd(test.GetComponent<Item>());
    }

    // Update is called once per frame
    public virtual void Update()
    {
        sMachine.updateState();
    }

    public void runBufss()
    {
        foreach (Buff buff in buffs)
        {
            Debug.Log(buff.durate);
            buff.durateEffect();
        }

        desetBuffs();
    }

    public void desetBuffs()
    {
        foreach (Buff buff in endBuffs)
        {
            buff.endEffect();

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

    public void DieObject()
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
}
