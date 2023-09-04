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

    public bool isHit = false;
    public float hitTime = 0;

    public int invensize = 12;
    public int currentInvenSize = 0;

    public int equipsize = 4;
    public int currentequipSize = 0;

    public int weaponsize = 2;
    public int currentweaponSize = 0;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        status = new SerializedPlayerStatus
        {
            buffs = new List<Buff>(),
            endbuffs = new List<Buff>(),
            inventory = new List<ItemSlotChara>(),
            weapons = new List<WeaponSlotChara>(),
            equips = new List<EquipSlotChara>(),
            element = new ElementData(),
        };
        sMachine = new StateMachine(this);

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
        for(int i = 0; i < 8; i++)
        {
            status.inventory.Add(new ItemSlotChara());
        }
        for (int i = 0; i < 4; i++)
        {
            status.equips.Add(new EquipSlotChara());
        }
        for (int i = 0; i < 2; i++)
        {
            status.weapons.Add(new WeaponSlotChara());
        }
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
    public virtual void FixedUpdate()
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
        if (!isHit)
        {
            Debug.Log("Hit");
            isHit = true;
            status.currentHP -= pts;

            if (status.currentHP <= 0)
            {
                DieObject();
            }
            Invoke("reclusiveHitbox", 1f);
        }
    }

    public void reclusiveHitbox()
    {
        isHit = false;
    }

    public virtual void DieObject()
    {
        GameManager.Resource.Destroy(this.gameObject);
    }

    public bool ItemAdd(Item item)
    {
        if (currentInvenSize < 8)
        {
            for(int i = 0; i < 8; i++)
            {
                if(status.inventory[i].item == null)
                {
                    status.inventory[i].AddItem(item);
                    break;
                }
            }
            GameManager.Instance.uiManager.invenFrest();
            return true;
        }
        else
        {
            Debug.Log("Inventory is Full");
            GameManager.Instance.uiManager.textUI("Inventory is Full");
            return false;
        }
    }

    public void ItemDelete(int n)
    {

    }

    public void changeState(State state)
    {
        sMachine.changeState(state);
    }

    public void endCurrentState()
    {
        sMachine.exitState();
    }

    public void setIdleState()
    {
        while(sMachine.getStateStr() != "MobIdleState" || sMachine.getStateStr() != "PlayerIdleState")
        {
            endCurrentState();
        }
    }

    public State getState()
    {
        return sMachine.getState();
    }
}
