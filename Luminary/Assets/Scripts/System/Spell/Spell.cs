using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour, Command
{
    public float cd = 0f;
    public virtual void execute() 
    {
    }

    public virtual float getCD()
    {
        return cd;
    }

}
