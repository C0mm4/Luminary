using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public int index = 0;
    public int menusize;


    public abstract void moveMenu();
}
