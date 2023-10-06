using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUI : Menu
{
    public NPC npc;
    [SerializeField]
    public GameObject TextUI;
    public GameObject SelectUI;

    public int selectIndex;


    public override void InputAction()
    {

    }

    public override void hide()
    {
        npc.isActivate = false;
        base.hide();
    }
}
