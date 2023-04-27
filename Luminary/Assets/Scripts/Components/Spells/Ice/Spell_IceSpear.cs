using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_IceSpear : Spell
{
    public override void set()
    {
        circle = 2;
        types = 2;
        cd = 3f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
