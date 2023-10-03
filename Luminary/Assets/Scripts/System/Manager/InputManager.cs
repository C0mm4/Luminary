using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action KeyAction = null;
    public Vector3 mousePos = new Vector3(), mouseWorldPos = new Vector3();
    private bool hasInput = false;
    public bool isDragging = false;

    public void OnUpdate()
    {
        if(KeyAction != null)
            KeyAction();
           
    }

    public void Update()
    {
        OnUpdate();
        mousePos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;

        if(GameManager.gameState != GameState.Loading && GameManager.uiState == UIState.InPlay)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.pauseGame();
            }
        }
    }

    public bool HasInput()
    {
        return hasInput;
    }

    public void changeInputState()
    {
        KeyAction = null;
        switch (GameManager.uiState)
        {
            case UIState.Title:
                KeyAction += TitleInput;
                break;
            case UIState.Lobby:
                LobbyInput();
                break;
            case UIState.InPlay:
                InGameInput(); 
                break;
            case UIState.Inventory:
                break;
            case UIState.Menu:

                break;
            case UIState.Setting:

                break;
            case UIState.CutScene: 
                
                break;
            case UIState.Pause:
                break;
        }
    }

    public void TitleInput()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            GameManager.Instance.uiManager.ChangeState(UIState.Loading);
            GameManager.Instance.sceneControl("LobbyScene");
        }
    }

    public void LobbyInput()
    {
        KeyAction += GameManager.Instance.uiManager.InventoryToggleInput;
    }

    public void InGameInput()
    {
        KeyAction += GameManager.Instance.uiManager.InPlayInput;
        KeyAction += GameManager.player.GetComponent<Player>().spellKey;
    }



}
