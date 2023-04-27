using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_StoneFist : Spell
{
    public override void set()
    {
        circle = 4;
        types = 4;
        cd = 10f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
