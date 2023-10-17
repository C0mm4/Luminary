using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpellData
{
    // Spell Name
    public string name;
    // Spell Circle
    public int circle;
    // Projectile Range
    public float xRange, yRange;
    // isProjectile or FIeld
    public int type;
    // DMG, HIts
    public int damage, hits;
    // Projectile Numbers
    public int projectileN;
    // castingTime
    public float castTime;
    // Field Type Durate Time
    public float durateT;
    // Projectile speed
    public float spd;
    // Prefab Path
    public string path;
    // Sprite Data
    public Sprite spr;
}
