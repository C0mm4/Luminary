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
    public MobData data;
    AIModel model;

    GameObject AtkPrefab;

    public Vector2 sawDirect;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        
        sMachine.changeState(new MobIdleState());
        try
        {
            player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();
        }
        catch
        {
            sMachine.changeState(new MobIdleState());
        }
        AIGen();
        Debug.Log(sMachine.getStateStr());
    }

    public void AIGen()
    {
        Type T = Type.GetType(data.AI);
        model = Activator.CreateInstance(T) as AIModel;
        model.target = this;
        Debug.Log(model.GetType().Name);
    }

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
        model.Update();


        // For Test
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Buff buff = new Ignite(this, this, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Buff buff = new Freeze(this, this, 0);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Buff buff = new Flow(this, this, 0);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Buff buff = new Shock(this, this, 0);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Buff buff = new Electric(this, this, 0);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Buff buff = new Seed(this, this, 0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Buff buff = new Sentence(this, this, 1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Buff buff = new Judgement(this, this, 1);
        }
    }

    public override void DieObject()
    {
        /*        GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().mobCount -= 1;
                if(GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().mobCount == 0)
                {
                    GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().clearRoom();
                }*/
        Debug.Log("Die");
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

    public Vector2 playerDistance()
    {

        Vector2 ret = new Vector2(Math.Abs(player.transform.position.x - transform.position.x), Math.Abs(player.transform.position.y - transform.position.y));
        return ret;
    }

    public Vector2 playerDir()
    {
        Vector2 ret = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y));
        return ret;
    }

    public void Attack(int index)
    {
        AtkPrefab = GameManager.Resource.Instantiate(attackPrefab[index], transform);
    }

    public void attakEnd()
    {
        if(getState().GetType().Name == "MobATKState")
        {
            endCurrentState();
            GameManager.Resource.Destroy(AtkPrefab);
        }
    }
}
