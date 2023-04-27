using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_StoneShower : Spell
{
    public override void set()
    {
        circle = 2;
        types = 4;
        cd = 2.5f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
