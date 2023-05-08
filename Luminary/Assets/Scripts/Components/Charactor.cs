using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    // Charactor Base Status
    protected int MaxHP;
    public int HPIncrease;
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
    void Start()
    {
        buffs = new List<Buff>();
        endBuffs = new List<Buff> ();
        items = new List<Item>();
        sMachine = new StateMachine();
        
    }

    // Update is called once per frame
    public void Update()
    {

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
}
