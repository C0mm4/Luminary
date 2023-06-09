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

                sMachine.changeState(new MobIdleState());
            }

        }
    }

    public override void DieObject()
    {
        GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().mobCount -= 1;
        if(GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().mobCount == 0)
        {
            GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<Room>().clearRoom();
        }
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
}
