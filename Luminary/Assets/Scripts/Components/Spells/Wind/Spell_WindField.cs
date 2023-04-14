using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_WindField : Spell
{
    public override void set()
    {
        circle = 4;
        types = 3;
        cd = 10f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
