using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public int index = 0;
    public int menusize;

    public virtual void Start()
    {
        GameManager.Instance.uiManager.addMenu(this);
        Debug.Log("Menu Generate");
    }

    public void hide()
    {
        GameManager.inputManager.KeyAction -= InputAction;
        GameManager.inputManager.KeyAction -= ESCInput;
        GameManager.Instance.uiManager.endMenu();
        gameObject.SetActive(false);
    }
    public virtual void show()
    {
        gameObject.SetActive(true);

        StartCoroutine(inputSet());
    }

    public IEnumerator inputSet()
    {
        yield return new WaitForSeconds(1f);
        GameManager.inputManager.KeyAction += InputAction;
        GameManager.inputManager.KeyAction += ESCInput;
    }

    public abstract void InputAction();

    public void ESCInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            hide();
        }
    }
}
