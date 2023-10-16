using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : Charactor
{
    // Mob Attack Prefab
    [SerializeField]
    public GameObject[] attackPrefab;
    public bool[] isHitbox;
    public MobData data;
    AIModel model;
    public float lastAttackT;

    public GameObject AtkObj;

    public Vector2 sawDirect;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        // Set Idle State
        sMachine.changeState(new MobIdleState());

        // Find Player
        try
        {
            player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();
        }
        catch
        {
            sMachine.changeState(new MobIdleState());
        }
        AIGen();
        
    }

    // AI Generate by string name
    public void AIGen()
    {
        Type T = Type.GetType(data.AI);
        model = Activator.CreateInstance(T) as AIModel;
        model.target = this;
        Debug.Log(model.GetType().Name);
    }

    // status initialize based on data
    public override void statusInit()
    {
        status.baseHP = data.baseHP;
        status.baseDMG = data.baseDMG;
        status.basespeed = data.basespeed;

        base.statusInit();
    }


    public override void FixedUpdate()
    {
        
        base.FixedUpdate();
        // if Player didn't find, research player object
        if(player == null)
        {
            try
            {
                player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();
            }
            catch
            {
                if(getState().GetType().Name != "MobStunState")
                {
                    sMachine.changeState(new MobIdleState());
                }
            }
        }

        // AI model update
        model.Update();


    }

    public override void DieObject()
    {
        // Current Room's mob count decrease
        /*        GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().mobCount -= 1;
                if(GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().mobCount == 0)
                {
                    GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().clearRoom();
                }*/
        GameManager.Resource.Destroy(AtkObj);
        base.DieObject();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player")
        {
            Debug.Log("Player Collision");
            other.GetComponent<Charactor>().HPDecrease(1);
        }
    }

    // return player Distance on Vector2
    public Vector2 playerDistance()
    {

        Vector2 ret = new Vector2(Math.Abs(player.transform.position.x - transform.position.x), Math.Abs(player.transform.position.y - transform.position.y));
        return ret;
    }

    // return player Direction on Vector2
    public Vector2 playerDir()
    {
        Vector2 ret = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y));
        return ret;
    }

    public void ProjectileGen(int index)
    {
        AtkObj = GameManager.Resource.Instantiate(attackPrefab[index]);
        AtkObj.GetComponent<MobProjectile>().setData(this);
    }

    public void FieldGen(int index)
    {
        AtkObj = GameManager.Resource.Instantiate(attackPrefab[index]);
        AtkObj.GetComponent<MobField>().setData(this);
    }

    // Generate Attack HitBox Prefab
    public void Attack(int index)
    {
        if (isHitbox[index])
        {
            AtkObj = GameManager.Resource.Instantiate(attackPrefab[index], transform);
        }
        else
        {
            if(AtkObj == null)
            {
                AtkObj = GameManager.Resource.Instantiate(attackPrefab[index]);
            }
        }
    }

    public void FieldProjectileActive()
    {
        try
        {
            // if obj is projectile Throw projectile object
            AtkObj.GetComponent<MobProjectile>().Throw();
        }
        catch
        {
            try
            {
                // if obj is Fild, Activate Field attack
                AtkObj.GetComponent<MobField>().setActive();
            }
            catch
            {
                // hitbox is destroy
                GameManager.Resource.Destroy(AtkObj);

            }
        }
    }

    // Attack State End handler Control in Animation Event
    public void attakEnd()
    {
        lastAttackT = Time.time;
        if(getState().GetType().Name == "MobATKState")
        {
            GameManager.Resource.Destroy(AtkObj);
            endCurrentState();
/*            try
            {
                // if obj is projectile Throw projectile object
                AtkObj.GetComponent<MobProjectile>().Throw();
            }
            catch
            {
                try
                {
                    // if obj is Fild, Activate Field attack
                    AtkObj.GetComponent<MobField>().setActive();
                }
                catch
                {
                    // hitbox is destroy
                    GameManager.Resource.Destroy(AtkObj);

                }
            }*/
        }
    }
}
