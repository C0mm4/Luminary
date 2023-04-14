using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour, Command
{
    public float cd = 0f;
    public int circle = 0;
    public int types = 0; // 1 = Fire 2 = Ice 3 = Wind 4 = Earth 5 = None
    public float xRange, yRange;
    public float damage, hits;
    public float castingTime;

    public GameObject obj;

    public virtual void set()
    {

    }

    public virtual void execute() 
    {

    }

    public virtual float getCD()
    {
        return cd;
    }

}
