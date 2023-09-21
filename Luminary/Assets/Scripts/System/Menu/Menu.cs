using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public int index = 0;
    public int menusize;

    public void Start()
    {
        GameManager.Instance.uiManager.addMenu(this);
        show();
    }

    public void hide()
    {
        GameManager.inputManager.KeyAction -= InputAction;
        GameManager.Instance.uiManager.endMenu();
        gameObject.SetActive(false);
    }
    public void show()
    {
        gameObject.SetActive(true);
        GameManager.Instance.uiManager.ChangeState(UIState.Menu);
        GameManager.inputManager.KeyAction += InputAction;
    }

    public abstract void InputAction();
}
