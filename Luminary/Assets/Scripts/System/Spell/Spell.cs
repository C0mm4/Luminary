using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public SpellData data;


    public bool isCool = false;
    public float ct, st;


    public virtual void execute() 
    {
        GameObject obj = GameManager.Resource.Instantiate(data.path);
        obj.GetComponent<SpellObj>().setData(data);
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
