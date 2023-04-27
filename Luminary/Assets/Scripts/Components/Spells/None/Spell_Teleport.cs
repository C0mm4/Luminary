using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Teleport : Spell
{
    public override void set()
    {
        circle = 0;
        types = 5;
        cd = 8f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
