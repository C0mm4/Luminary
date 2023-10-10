using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public int index = 0;
    public int menusize;
    public int currentMenu;

    public virtual void Start()
    {
        GameManager.Instance.uiManager.addMenu(this);
        Debug.Log("Menu Generate");
    }

    public virtual void hide()
    {
        GameManager.inputManager.KeyAction -= InputAction;
        GameManager.inputManager.KeyAction -= ESCInput;
        gameObject.SetActive(false);
    }
    public virtual void show()
    {
        gameObject.SetActive(true);

        StartCoroutine(inputSet());
    }

    public virtual void exit()
    {
        hide();
        GameManager.Resource.Destroy(gameObject);
    }

    public IEnumerator inputSet()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.inputManager.KeyAction += InputAction;
        GameManager.inputManager.KeyAction += ESCInput;
    }

    public abstract void InputAction();

    public void ESCInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.uiManager.endMenu();
        }
    }
}
