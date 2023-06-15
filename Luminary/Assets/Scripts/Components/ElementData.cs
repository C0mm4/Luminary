using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementData
{
    public bool Fire, Ice, Wind, Rock, FireIce, FireWind, FireRock, IceWind, IceRock, WindRock;
    public void init()
    {
        Fire = false; Ice = false; Wind = false; Rock = false;
        FireIce = false; FireWind = false; FireRock = false;
        IceWind = false; IceRock = false; WindRock = false;
    }

    public void onCheck()
    {

    }
}
