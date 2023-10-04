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

    public float detectDistance;

    public float attackRange;

    public float castSpeed;

    public List<Item> items;

    Sprite spr;

    public string AI;

    public string Atk;
}
