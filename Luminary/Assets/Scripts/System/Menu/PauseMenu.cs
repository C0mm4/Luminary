using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    public void Start()
    {
        GameManager.Instance.uiManager.addMenu(this);
        GameManager.inputManager.KeyAction += moveMenu;
    }

    public override void moveMenu()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
            index %= menusize;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            index--;
            index %= menusize;
        }
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            switch (index)
            {
                case 0:
                    GameManager.Instance.pauseGame();
                    GameManager.inputManager.KeyAction -= moveMenu;
                    break;
                case 1:
                    // Menu Set
                    break;
                case 2:
                    // Save And End
                    break;

            }
        }
    }
}
