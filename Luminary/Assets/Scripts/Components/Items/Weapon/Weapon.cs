using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ItemFunc
{
    public int effectLevel = 1;
    public int spellLevel = 1;

    public int spellType;
    public int currentSpellNum;
    public int currentSpellLevel;

    
    
    public void EffectUpgrade()
    {

    }

    public void SpellUpgrade()
    {

    }
}
