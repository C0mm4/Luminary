using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action KeyAction = null;
    public Vector3 mousePos = new Vector3(), mouseWorldPos = new Vector3();
    private bool hasInput = false;

    public void OnUpdate()
    {
        if (Input.anyKey)
        {
            hasInput = true;
            if (KeyAction != null)
            {
                KeyAction();
            }

        }
        else
        {
            hasInput = false;
        }
    }

    public void Update()
    {
        OnUpdate();
        mousePos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;

        if(GameManager.gameState != GameState.Loading)
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
                InventoryInput();
                break;
            case UIState.Menu:

                break;
            case UIState.Setting:

                break;
            case UIState.CutScene: 
                
                break;
            case UIState.Pause:
                PauseInput();
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
        KeyAction += GameManager.player.GetComponent<Player>().spellKey;
    }

    public void InGameInput()
    {
        KeyAction += GameManager.Instance.uiManager.InPlayInput;
    }

    public void MenuInput()
    {
        KeyAction += GameManager.Instance.uiManager.MenuInput;
    }

    public void PauseInput()
    {
        KeyAction += GameManager.Instance.uiManager.PauseInput;
    }

    public void InventoryInput()
    {
        KeyAction += GameManager.Instance.uiManager.InventoryToggleInput;
        KeyAction += GameManager.Instance.uiManager.InventoryInInput;
    }
}
