using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public GameObject button;

    public void gameInit()
    {
        GameManager.Instance.sceneControl("LobbyScene");
    }
    public void inStage()
    {
        GameManager.Instance.sceneControl("Stage (MapGenerator Demo)");
    }
}
