using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Charactor : MonoBehaviour
{
    // Charactor State Machine
    protected StateMachine sMachine;

    // player instance
    public Charactor player;

    // Element State Data
    public ElementData element;

    public SerializedPlayerStatus status;


    // Start is called before the first frame update
    public virtual void Start()
    {
        status.buffs = new List<Buff>();
        status.endbuffs = new List<Buff> ();
        status.items = new List<Item>();
        status.equips = new List<Item>();
        sMachine = new StateMachine();
        element = new ElementData();

        statusInit();
    }

    public virtual void statusInit()
    {
        // Charactor base Status
        // overriding this func and reset base status values
        /*
        status.baseHP = 10;
        status.baseDMG = 1;
        status.basespeed = 5;
        */

        status.increaseDMG = 0;
        status.increaseSpeed = 0;
        status.increseMaxHP = 0;
        status.pIncreaseDMG = 0;
        status.pIncreaseMaxHP = 0;
        status.pIncreaseSpeed = 0;
        status.pGetDMG = 1;
        status.level = 1;
        calcStatus();
        status.currentHP = status.maxHP;
        Debug.Log(status.maxHP);
        Debug.Log(status.currentHP);
    }

    public virtual void calcStatus()
    {
        // HP status Calculate
        status.maxHP = (int)Math.Round((status.baseHP + status.increseMaxHP) * (status.pIncreaseMaxHP+1));

        // damage Calculate
        status.finalDMG = (int)Math.Round((status.baseDMG + status.increaseDMG) * (status.pIncreaseDMG+1));

        // speed Calculate
        status.speed = (int)Math.Round((status.basespeed + status.increaseSpeed) * (status.pIncreaseSpeed+1));
    }

    // Update is called once per frame
    public virtual void Update()
    {
        sMachine.updateState();
        runBufss();
    }

    public void runBufss()
    {
        foreach (Buff buff in status.buffs)
        {
            if (buff != null)
            {
                buff.durateEffect();
            }
        }

        desetBuffs();
    }

    public void desetBuffs()
    {
        foreach (Buff buff in status.endbuffs)
        {
            if (buff != null)
            {
                buff.endEffect();
            }

        }
        status.endbuffs.Clear();
    }

    public void HPIncrease(int pts)
    {
        status.currentHP += pts;
        if(status.currentHP >= status.maxHP)
        {
            status.currentHP = status.maxHP;
        }
    }

    public void HPDecrease(int pts)
    { 
        status.currentHP -= pts;
        Debug.Log(pts + " get damage " + status.currentHP);
        if(status.currentHP <= 0)
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
        if (status.items.Count < 8)
        {
            status.items.Add(item);
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }

    public void ItemEqip(Item item) 
    {
        if (status.equips.Count < 4)
        {
            status.equips.Add(item);
            status.items.Remove(item);
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Full Equiped");
        }
    }

    public void ItemUnequip(Item item)
    {
        if (status.items.Count < 8)
        {
            status.equips.Remove(item);
            status.items.Add(item);
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }
}
