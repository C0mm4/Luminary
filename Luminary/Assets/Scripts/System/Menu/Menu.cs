using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public int index = 0;
    public int menusize;
    public int currentMenu;

    // Set Menu Stack on UIManager
    public virtual void Start()
    {
        GameManager.Instance.uiManager.addMenu(this);
        Debug.Log("Menu Generate");
    }

    // when Menu hide, input deset, gameObject SetActive false
    public virtual void hide()
    {
        GameManager.inputManager.KeyAction -= InputAction;
        GameManager.inputManager.KeyAction -= ESCInput;
        gameObject.SetActive(false);
    }
    // when Menu show, input Set, gameObject Active true
    public virtual void show()
    {
        gameObject.SetActive(true);

        StartCoroutine(inputSet());
    }

    // when Menu Exit, Input Deset, GameObject Destroy
    public virtual void exit()
    {
        hide();
        GameManager.Resource.Destroy(gameObject);
    }

    // menu input Set Delayed 0.5f seconds
    public IEnumerator inputSet()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.inputManager.KeyAction += InputAction;
        GameManager.inputManager.KeyAction += ESCInput;
    }

    // when menu is activate, set Key Inputs
    public abstract void InputAction();

    // some menus have confirm button. that buttons action
    public abstract void ConfirmAction();

    // ESC Input = exit
    public void ESCInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.uiManager.endMenu();
        }
    }
}
