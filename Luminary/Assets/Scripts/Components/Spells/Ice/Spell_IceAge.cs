using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_IceAge : Spell
{
    public override void set()
    {
        circle = 4;
        types = 2;
        cd = 18f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
