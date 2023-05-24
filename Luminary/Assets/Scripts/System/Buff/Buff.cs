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

    public void setDurate(int d)
    {
        durate = d;
    }

    public void startEffect()
    {
        // Extention Classes Doesn't Call
        int index = target.buffs.FindIndex(buff => buff.id == id);
        if (index == -1)
        {
            Debug.Log(durate);
            Debug.Log("INPUT LIST");
            target.buffs.Add(instance);
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
        // on Tick Buffs
        // Extenttion Contents
        //
        // base.durateEffect();


        Debug.Log(durate);
        Debug.Log("INDURATE");

        currentTime = Time.time - startTime;
        if (currentTime - lastTickTime > tickTime)
        {
            onTick();
            lastTickTime = currentTime;

        }
        if(currentTime >= durate)
        {
            target.endBuffs.Add(instance);
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

        target.buffs.RemoveAt(i);
        target.buffs.Add(instance);
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
