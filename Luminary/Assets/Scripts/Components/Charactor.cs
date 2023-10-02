using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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

    public bool isboss;

    public bool canMove = true;

    public bool isHit = false;
    public float hitTime = 0;

    public int currentInvenSize = 0;

    public int currentequipSize = 0;

    public int currentweaponSize = 0;

    public Event attackEffect = null;
    public Event hitEffect = null;

    [SerializeField]
    public bool godmode;

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
        for (int i = 0; i < 8; i++)
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
        status.maxHP = (int)((status.baseHP + status.strength / 2) * (status.pIncreaseMaxHP + 1));

        // damage Calculate
        status.finalDMG = (int)Math.Round((status.baseDMG * ((1 + (0.1 * status.Intellect))) + (0.02 * status.strength) + (0.03 * status.dexterity) + (1 + status.increaseDMG) / 100));

        // speed Calculate
        status.speed = (int)Math.Round((status.basespeed + status.increaseSpeed) * ((status.dexterity * 0.05) + 0.95) * (status.pIncreaseSpeed + 1));
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

    public void setBuff(Buff buff)
    {
        if (!isbuffCool(buff.id))
        {
            buff.target = this;
            status.buffs.Add(buff);
            buff.startEffect();
            Debug.Log("Set Buffs");
            Debug.Log(status.buffs.Count);
        }
    }

    public bool isbuffCool(int id)
    {
        switch (id)
        {
            case 0:
                return status.element.ignite;
            case 1:
                return status.element.freeze;
            case 2:
                return status.element.flow;
            case 3:
                return status.element.shock;
            case 4:
                return status.element.electric;
            case 5:
                return status.element.seed;
            case 6:
                return status.element.sentence;
            case 7:
                return status.element.judgement;
            case 8:
                return status.element.darkness;
            case 9:
                return status.element.clean;
            case 10:
                return status.element.melting;
            case 11:
                return status.element.burning;
            case 12:
                return status.element.extinguish;
            case 13:
                return status.element.electfire;
            case 14:
                return status.element.fire;
            case 15:
                return status.element.expand;
            case 16:
                return status.element.cracked;
            case 17:
                return status.element.electshock;      
            case 18:    
                return status.element.rooted;
            case 19:
                return status.element.weathering;
            case 20:
                return status.element.overloading;
            case 21:
                return status.element.diffusion;
            case 22:
                return status.element.discharge;
            case 23:
                return status.element.sprout;
            case 24:
                return status.element.boost;
            case 25:
                return status.element.execution;
        }
        return true;
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
        Debug.Log(gameObject.name + " Get Dmg " + pts);
        if (!godmode)
        {
            if (gameObject.tag == "Player")
            {
                if (!isHit)
                {
                    Debug.Log("Hit");
                    isHit = true;
                    status.currentHP -= pts;

                    Invoke("reclusiveHitbox", 1f);
                }
            }
            else
            {
                double dmg = pts * (100 - status.def) / 100;
                status.currentHP -= (int)Math.Floor(dmg);
            }
            if (status.currentHP <= 0)
            {
                DieObject();
            }
        }
    }

    public void TrueDMG(int pts)
    {
        Debug.Log(gameObject.name + " Get True Dmg " + pts);
        if (!godmode)
        {
            status.currentHP -= pts;
            if (status.currentHP <= 0)
            {
                DieObject();
            }
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

    public bool ItemAdd(Item item, int index = -1)
    {
        if(index == -1)
        {
            if (currentInvenSize < 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (status.inventory[i].item == null)
                    {
                        status.inventory[i].AddItem(item);
                        break;
                    }
                }
                GameManager.Instance.uiManager.invenFresh();
                Debug.Log(item.data.type);
                return true;
            }
            else
            {
                Debug.Log("Inventory is Full");
                GameManager.Instance.uiManager.textUI("Inventory is Full");
                return false;
            }
        }
        else
        {
            status.inventory[index].AddItem(item);
            return true;
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

    public void buffCool(float cd, int id)
    {
        StartCoroutine(buffcooltime(cd, id));
    }

    public IEnumerator buffcooltime(float cd, int id)
    {
        yield return new WaitForSeconds(cd);
        switch (id)
        {
            case 0:
                status.element.ignite = false;
                break;
            case 1:
                status.element.freeze = false;
                break;
            case 2:
                status.element.flow = false;
                break;
            case 3:
                status.element.shock = false;
                break;
            case 4:
                status.element.electric = false;
                break;
            case 5:
                status.element.seed = false;
                break;
            case 6:
                status.element.sentence = false;
                break;
            case 7:
                status.element.judgement = false;
                break;
            case 8:
                status.element.darkness = false;
                break;
            case 9:
                status.element.clean = false;
                break;
            case 10:
                status.element.melting = false;
                break;
            case 11:
                status.element.burning = false;
                break;
            case 12:
                status.element.extinguish = false;
                break;
            case 13:
                status.element.electfire = false;
                break;
            case 14:
                status.element.fire = false;
                break;
            case 15:
                status.element.expand = false;
                break;
            case 16:
                status.element.cracked = false;
                break;
            case 17:
                status.element.electshock = false;
                break;
            case 18:
                status.element.rooted = false;
                break;
            case 19:
                status.element.weathering = false;
                break;
            case 20:
                status.element.overloading = false;
                break;
            case 21:
                status.element.diffusion = false;
                break;
            case 22:
                status.element.discharge = false;
                break;
            case 23:
                status.element.sprout = false;
                break;
            case 24:
                status.element.boost = false;
                break;
            case 25:
                status.element.execution = false;
                break;
        }
    }

    public void ItemStatusSum(SerializeItemStatus itemdata)
    {

        status.dexterity += itemdata.dex;
        status.strength += itemdata.strength;
        status.Intellect += itemdata.intellect;


        status.increaseDMG += itemdata.increaseDMG;
        status.pIncreaseDMG += itemdata.pincreaseDMG;

        status.igniteDMG += itemdata.igniteDMG;
        status.freezeDMG += itemdata.freezeDMG;
        status.flowDMG += itemdata.flowDMG;
        status.shockDMG += itemdata.shockDMG;
        status.electDMG += itemdata.electDMG;
        status.seedDMG += itemdata.seedDMG;

        status.meltingDMG += itemdata.meltingDMG;
        status.extinguishDMG += itemdata.extinguishDMG;
        status.fireDMG += itemdata.fireDMG;
        status.electFireDMG += itemdata.electFireDMG;
        status.burnningDMG += itemdata.burnningDMG;
        status.crackedDMG += itemdata.crackedDMG;
        status.rootedDMG += itemdata.rootedDMG;
        status.electShockDMG += itemdata.electShockDMG;
        status.expandDMG += itemdata.expandDMG;
        status.sproutDMG += itemdata.sproutDMG;
        status.dischargeDMG += itemdata.dischargeDMG;
        status.weatheringDMG += itemdata.weatheringDMG;
        status.boostDMG += itemdata.boostDMG;
        status.diffusionDMG += itemdata.diffusionDMG;
        status.overloadDMG += itemdata.overloadDMG;
        status.executionDMG += itemdata.executionDMG;

        status.pGetDMG += itemdata.pGetDMG;


        status.increseMaxHP += itemdata.increaseHP;
        status.pIncreaseMaxHP += itemdata.pincreaseHP;
            
        status.increaseMaxMana += itemdata.increaseMP;
        status.pIncreaseMaxMana += itemdata.pincreaseMP;

        status.increaseSpeed += itemdata.increaseSpeed;
        status.pIncreaseSpeed += itemdata.pincreaseSpeed;
    }


    public void ItemStatusminus(SerializeItemStatus itemdata)
    {

        status.dexterity -= itemdata.dex;
        status.strength -= itemdata.strength;
        status.Intellect -= itemdata.intellect;


        status.increaseDMG -= itemdata.increaseDMG;
        status.pIncreaseDMG -= itemdata.pincreaseDMG;

        status.igniteDMG -= itemdata.igniteDMG;
        status.freezeDMG -= itemdata.freezeDMG;
        status.flowDMG -= itemdata.flowDMG;
        status.shockDMG -= itemdata.shockDMG;
        status.electDMG -= itemdata.electDMG;
        status.seedDMG -= itemdata.seedDMG;

        status.meltingDMG -= itemdata.meltingDMG;
        status.extinguishDMG -= itemdata.extinguishDMG;
        status.fireDMG -= itemdata.fireDMG;
        status.electFireDMG -= itemdata.electFireDMG;
        status.burnningDMG -= itemdata.burnningDMG;
        status.crackedDMG -= itemdata.crackedDMG;
        status.rootedDMG -= itemdata.rootedDMG;
        status.electShockDMG -= itemdata.electShockDMG;
        status.expandDMG -= itemdata.expandDMG;
        status.sproutDMG -= itemdata.sproutDMG;
        status.dischargeDMG -= itemdata.dischargeDMG;
        status.weatheringDMG -= itemdata.weatheringDMG;
        status.boostDMG -= itemdata.boostDMG;
        status.diffusionDMG -= itemdata.diffusionDMG;
        status.overloadDMG -= itemdata.overloadDMG;
        status.executionDMG -= itemdata.executionDMG;

        status.pGetDMG -= itemdata.pGetDMG;


        status.increseMaxHP -= itemdata.increaseHP;
        status.pIncreaseMaxHP -= itemdata.pincreaseHP;

        status.increaseMaxMana -= itemdata.increaseMP;
        status.pIncreaseMaxMana -= itemdata.pincreaseMP;

        status.increaseSpeed -= itemdata.increaseSpeed;
        status.pIncreaseSpeed -= itemdata.pincreaseSpeed;
    }
}
