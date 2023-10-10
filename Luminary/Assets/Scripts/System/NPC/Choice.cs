using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Choice : MonoBehaviour
{
    public int index;
    [SerializeField]
    public Sprite select;
    public Sprite deSelect;
    // Start is called before the first frame update
    public abstract void Work();
}
