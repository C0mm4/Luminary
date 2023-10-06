using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractionTrriger
{
    public bool isActivate;
    public GameObject openmenu;
    [SerializeField]
    public GameObject menu;
    [SerializeField]
    public bool isSelect;
    [SerializeField]

    public override void isInteraction()
    {
        openmenu = GameManager.Resource.Instantiate(menu);
        openmenu.GetComponent<NPCUI>().npc = this;
        isActivate = true;
        base.isInteraction();
    }


    public override void Update()
    {
        base.Update();
        if (isActivate)
        {
            if(menu != null)
            {
                menu.GetComponent<NPCUI>();
            }
        }
    }
}
