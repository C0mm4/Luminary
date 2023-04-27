using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Rock : Spell
{
    public override void set()
    {
        circle = 1;
        types = 4;
        cd = 1.5f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
