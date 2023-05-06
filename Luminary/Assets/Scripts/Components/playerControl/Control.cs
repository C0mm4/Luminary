using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Control : Charactor
{
    Behavior behavior;

    protected bool cdRoll, cdQ, cdW, cdE, cdR;
    protected Vector3 mousePos, transPos, targetPos;



    protected Inventory inven;

    void Awake()
    {
        behavior = GetComponent<Behavior>();
        player = this;
    }

    protected virtual void Update()
    {
        runBufss();
        if (Input.GetMouseButton(1))
            calTargetPos();

        behavior.move();


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
            Debug.Log("\"I\" KEY INPUT");
        }
    }

    void calTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }


}