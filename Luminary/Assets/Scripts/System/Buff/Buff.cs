using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public Charactor target;
    public Charactor attacker;

    public float startTime;
    public float currentTime;
    public float leftTime;

    public float dmg;
    public float durate;

    public Buff instance;

    Sprite img;

    public int id;
    public Buff()
    {

    }
    public Buff(Charactor tar, Charactor atk)
    {
        // base.Buff(tar, atk)
        // 
        // Extention Contents
        
        target = tar;
        attacker = atk;
        instance = this;
        startEffect();
    }

    public void startEffect()
    {
        // Extention Classes Doesn't Call
        Debug.Log(target);

        if(target.buffs.FindIndex(buff => buff.id == id) == -1)
        {
            target.buffs.Add(instance);
        }
        startTime = Time.time;
    }

    public virtual void durateEffect()
    {
        // on Tick Buffs
        // Extenttion Contents
        //
        // base.durateEffect();

        Debug.Log("BUFF");
        currentTime = Time.time - startTime;
        if(currentTime >= durate)
        {
            target.endBuffs.Add(instance);
        }
    }

    public virtual void endEffect()
    {
        //
        // Extention Contents
        //
        // base.endEffect();

        int targetid = target.buffs.FindIndex(item => item.instance.Equals(instance));
        target.buffs.RemoveAt(targetid);
    }
}
