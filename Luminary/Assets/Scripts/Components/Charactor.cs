using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    // Charactor Base Status
    protected int MaxHP;
    protected int CurrentHP;
    [SerializeField]
    public float speed;
    protected int Level;

    // Charactor Buffs / Debufs
    protected List<Buff> buffs;

    // Charactor Items
    public List<Item> items;

    // Charactor State Machine
    protected StateMachine sMachine;

    // player instance
    public Charactor player;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
