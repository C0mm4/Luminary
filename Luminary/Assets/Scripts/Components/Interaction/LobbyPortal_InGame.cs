using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPortal_InGame : InteractionTrriger
{
    public void Start()
    {
        interactDist = 5f;
        text = "�̵��Ѵ�";
    }
    public override void isInteraction()
    {
        GameManager.Instance.sceneControl("StageScene");
        base.isInteraction();
    }
}
