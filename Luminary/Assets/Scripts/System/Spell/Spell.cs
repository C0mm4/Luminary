using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public SpellData data;

    public virtual void execute(Vector3 mos) 
    {
        GameObject obj = GameManager.Resource.Instantiate(data.path);
        obj.GetComponent<SpellObj>().setData(data, mos);

    }

    public virtual void execute(GameObject target)
    {
        GameObject obj = GameManager.Resource.Instantiate(data.path);
        obj.GetComponent<SpellObj>().setData(data, target);

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
