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
    public AIModel model;
    public List<float> lastAttackT = new List<float>();

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
        for(int i = 0; i < data.castSpeed.Count; i++)
        {
            lastAttackT.Add(0f);
        }
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
        model.FixedUpdate();


    }

    public override void DieObject()
    {
        // Current Room's mob count decrease
        GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<DunRoom>().mobCount -= 1;
        if(GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<DunRoom>().mobCount == 0)
        {
            GameManager.StageC.ClearRoom();
        }
        GameManager.Resource.Destroy(AtkObj);
        GameManager.player.GetComponent<Player>().status.gold += data.dropGold;
        base.DieObject();
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player")
        {

            other.gameObject.GetComponent<Charactor>().HPDecrease(1);
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
    // Projectile attack object generate
    public void ProjectileGen(int index)
    {
        AtkObj = GameManager.Resource.Instantiate(attackPrefab[index]);
        AtkObj.GetComponent<MobProjectile>().setData(this);
    }
    // field type attack object generate
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
    // activate projectile and field type attack object
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
    public void attakEnd(int n)
    {
        lastAttackT[n] = Time.time;
        if(getState().GetType().Name == "MobATKState")
        {
            GameManager.Resource.Destroy(AtkObj);
            endCurrentState();

        }
    }

    public float HPPercent()
    {
        return (float)((float)status.currentHP / (float)status.maxHP);
    }

    public void MoveSetPosition(float x, float y)
    {
        Vector3 Roompos = GameManager.StageC.rooms[GameManager.StageC.currentRoom].transform.position;
        Vector3 pos = new Vector3(Roompos.x + x, Roompos.y + y);
        transform.position = pos;
    }

    public void MovePosition(float x, float y)
    {
        transform.position += new Vector3(x, y);
    }

    public void SetHitBox()
    {
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<CircleCollider2D>().isTrigger = false;
    }

    public void DeSetHitBox()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
}