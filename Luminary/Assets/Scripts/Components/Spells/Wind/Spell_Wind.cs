using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Wind : Spell
{
    public override void set()
    {
        circle = 1;
        types = 3;
        cd = 2f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
