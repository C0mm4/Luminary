using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_WindScrew : Spell
{
    public override void set()
    {
        circle = 3;
        types = 3;
        cd = 6f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
