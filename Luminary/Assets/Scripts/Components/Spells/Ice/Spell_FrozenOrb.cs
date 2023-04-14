using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_FrozenOrb : Spell
{
    public override void set()
    {
        circle = 3;
        types = 2;
        cd = 9f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
