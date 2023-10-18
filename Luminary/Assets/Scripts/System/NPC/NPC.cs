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
    public int index;

    [SerializeField]
    public List<string> selections;

    public List<string> scripts = new List<string>();

    public List<Item> items = new List<Item>();
    public List<bool> takeALook = new List<bool>();

    public void Awake()
    {
        // Set Scripts
        scripts = GameManager.Instance.getTextData(index);
    }

    public override void isInteraction()
    {
        // interaction this object, NPC Dialog UI Generate
        openmenu = GameManager.Resource.Instantiate("UI/NPCUI/NPCDialog");
        openmenu.GetComponent<NPCUI>().npc = this;
        openmenu.GetComponent<NPCUI>().setData();
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
