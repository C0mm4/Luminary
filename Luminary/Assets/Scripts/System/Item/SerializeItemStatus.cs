using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SerializeItemStatus
{
    public int dex;
    public int strength;
    public int intellect;

    public int increaseDMG;
    public float pincreaseDMG;

    public int increaseHP;
    public float pincreaseHP;

    public int increaseMP;
    public float pincreaseMP;

    public float increaseSpeed;
    public float pincreaseSpeed;

    public float pGetDMG;

    public static SerializeItemStatus operator + (SerializeItemStatus left, SerializeItemStatus right)
    {
        return new SerializeItemStatus
        {
            dex = left.dex + right.dex,
            strength = left.strength + right.strength,
            intellect = left.intellect + right.intellect,

            increaseDMG = left.increaseDMG + right.increaseDMG,
            pincreaseDMG = left.pincreaseDMG + right.pincreaseDMG,

            increaseHP = left.increaseHP + right.increaseHP,
            pincreaseHP = left.pincreaseHP + right.pincreaseHP,

            increaseMP = left.increaseMP + right.increaseMP,
            pincreaseMP = left.pincreaseMP + right.pincreaseMP,

            increaseSpeed = left.increaseSpeed + right.increaseSpeed,
            pincreaseSpeed = left.pincreaseSpeed + right.pincreaseSpeed,

            pGetDMG = left.pGetDMG + right.pGetDMG
        };
    }

}
