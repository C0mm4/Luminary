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

    public float tickTime;
    public float lastTickTime;

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

    public void setDurate(float d)
    {
        durate = d;
    }

    public void setTickTime(float t)
    {
        tickTime = t;
    }

    public void startEffect()
    {
        // Extention Classes Doesn't Call
        int index = target.status.endbuffs.FindIndex(buff => buff.id == id);
        if (index == -1)
        {
            target.status.buffs.Add(instance);
        }
        else
        {
            resetEffect(index);
        }
        startTime = Time.time;
        lastTickTime = startTime;
    }

    public virtual void durateEffect()
    {

        // Extenttion Contents
        //
        // base.durateEffect();

        currentTime = Time.time;
        if(currentTime - startTime >= durate)
        {
            target.status.endbuffs.Add(instance);
        }
    }

    public virtual void onTick()
    {
    }

    public virtual void resetEffect(int i)
    {
        //
        // Extention Contents
        //
        // base.resetEffect();

        target.status.buffs.RemoveAt(i);
        target.status.buffs.Add(instance);
    }

    public virtual void endEffect()
    {
        //
        // Extention Contents
        //
        // base.endEffect();
        int targetid = target.status.buffs.FindIndex(item => item.instance.Equals(instance));
        target.status.buffs.RemoveAt(targetid);
    }
}
