using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Control : Charactor
{
    Behavior behavior;

    protected bool cdRoll, cdQ, cdW, cdE, cdR;
    protected Vector3 mousePos, transPos, targetPos;



    public override void Awake()
    {
        base.Awake();
        Debug.Log("Control Awake");
        behavior = GetComponent<Behavior>();
        player = this;
        GameManager.inputManager.KeyAction -= onKeyboard;
        GameManager.inputManager.KeyAction += onKeyboard;
//        base.status = PlayerDataManager.playerStatus;
        
        GameManager.Instance.SceneChangeAction += DieObject;
    }

    public override void Update()
    {
        base.Update();
        if (behavior == null )
        {
            behavior = GetComponent<Behavior>();
        }
        behavior.move();
    }

    void calTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Inventory inventory = new Inventory();
        if (other.gameObject.CompareTag("Item"))
        {
            GameObject item = other.gameObject;
            status.items.Add(other.GetComponent<Item>());
        }
    }

    public override void DieObject()
    {
        GameManager.inputManager.KeyAction -= onKeyboard;
        GameManager.Instance.SceneChangeAction -= DieObject;
        PlayerDataManager.playerStatus = status;
        base.DieObject();

    }

    void onKeyboard()
    {
        if (Input.GetMouseButton(1))
            calTargetPos();

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A pressed");
        }

        if (Input.GetKeyDown("space"))
        {
            if (cdRoll == false)
            {
                StartCoroutine("roll");
            }
        }

        if (Input.GetKeyDown("q"))
        {
            if (cdQ == false)
            {
                StartCoroutine("Q");
            }
        }

        if (Input.GetKeyDown("w"))
        {
            if (cdW == false)
            {
                StartCoroutine("W");
            }
        }

        if (Input.GetKeyDown("e"))
        {
            if (cdE == false)
            {
                StartCoroutine("E");
            }
        }

        if (Input.GetKeyDown("r"))
        {
            if (cdR == false)
            {
                StartCoroutine("R");
            }
        }

        if (Input.GetKeyDown("i"))
        {
            GameManager.Instance.interaction();
        }

        if (Input.GetKeyDown("s"))
        {
            Buff newbuff = new Testbuff(player, player);
        }
    }
}