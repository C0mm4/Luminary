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

    public List<float> attackRange = new List<float>(); // Player in attack Range, then casting attack

    public List<float> castSpeed = new List<float>(); // Casting Speed

    public List<float> castCool = new List<float>(); // Casting Cooltime (Attack end to next Attack Cast Start Time)

    public List<Item> items; // For Boss Mob, Drop Item Generate

    public string AI; // AI Model's name

    public int dropGold; // Drop Golds

}
