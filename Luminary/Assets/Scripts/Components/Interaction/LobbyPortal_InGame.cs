using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPortal_InGame : InteractionTrriger
{
    public void Start()
    {
        interactDist = 5f;
    }
    public override void isInteraction()
    {
        GameManager.Instance.sceneControl("StageScene");
        base.isInteraction();
    }
}
