using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_IcePrison : Spell
{
    public override void set()
    {
        circle = 5;
        types = 2;
        cd = 22f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
