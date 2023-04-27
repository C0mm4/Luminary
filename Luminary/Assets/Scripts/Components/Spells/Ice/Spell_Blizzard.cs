using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Blizzard : Spell
{
    public override void set()
    {
        circle = 5;
        types = 2;
        cd = 25f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
