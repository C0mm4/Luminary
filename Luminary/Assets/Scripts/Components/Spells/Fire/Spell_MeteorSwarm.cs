using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_MeteorSwarm : Spell
{
    public override void set()
    {
        circle = 4;
        types = 1;
        cd = 30f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
