using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    SpellData data;


    public bool isCool = false;
    public float ct, st;

    public virtual void set()
    {

    }

    public virtual void execute() 
    {
        GameManager.Resource.Instantiate(data.path);
    }

    public virtual float getCD()
    {
        return data.cd;
    }
    public Sprite getSpr()
    {
        return data.spr;
    }

    public void setData(SpellData dt)
    {
        data = dt;
    }
}
