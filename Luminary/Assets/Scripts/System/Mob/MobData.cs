using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MobData : ScriptableObject
{
    [SerializeField]
    public int index;

    public int baseHP; // base HP

    public int baseDMG; // base DMG

    public float basespeed; // base Speed

    public float runRange; // some AI models player too close run away

    public float runDistance; // RunRange < RunDistance < DetectDistance

    public float detectDistance; // Mob AI Detect Player Distance

    public float attackRange; // Player in attack Range, then casting attack

    public float castSpeed; 

    public List<Item> items; // For Boss Mob, Drop Item Generate

    public string AI; // AI Model's name

    public int dropGold; // Drop Golds

}
