using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    public void Start()
    {
        GameManager.Instance.uiManager.addMenu(this);
        GameManager.inputManager.KeyAction += moveMenu;
        menusize = 3;
    }

    public override void moveMenu()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
            index %= menusize;
            Debug.Log(index);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            index--;
            if(index < 0)
            {
                index = menusize - 1;
            }
            Debug.Log(index);
        }
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            switch (index)
            {
                case 0:
                    GameManager.Instance.pauseGame();
                    break;
                case 1:
                    // Menu Set
                    break;
                case 2:
                    // Save And End

                    GameManager.Instance.pauseGame();
                    break;

            }
        }
    }
    public void OnDestroy()
    {
        GameManager.inputManager.KeyAction -= moveMenu;
        GameManager.Instance.uiManager.endMenu();
    }
}
