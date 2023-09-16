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

    public int basespeed; // base Speed

    public List<Item> items;

    Sprite spr;
}
