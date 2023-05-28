using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action KeyAction = null;
    public InputState inputstate;
    public Vector3 mousePos = new Vector3(), mouseWorldPos = new Vector3();


    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }

    public void Update()
    {
        OnUpdate();
        mousePos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
    }
}
