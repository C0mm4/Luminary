using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPortal_InGame : InteractionTrriger
{
    public override void isInteraction()
    {
        GameManager.Instance.sceneControl("StageScene");
    }
}
