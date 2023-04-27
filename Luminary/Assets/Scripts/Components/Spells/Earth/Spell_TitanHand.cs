using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_TitanHand : Spell
{
    public override void set()
    {
        circle = 5;
        types = 4;
        cd = 22f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
