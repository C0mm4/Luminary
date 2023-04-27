using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Fire : Spell
{
    public override void set()
    {
        cd = 2f;
        circle = 1;
        types = 1; // 1 = Fire 2 = Ice 3 = Wind 4 = Earth 5 = None

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
