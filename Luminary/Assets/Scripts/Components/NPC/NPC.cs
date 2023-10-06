using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractionTrriger
{
    [SerializeField]
    public GameObject menu;
    public override void isInteraction()
    {
        GameManager.Resource.Instantiate(menu);
        base.isInteraction();
    }

}
