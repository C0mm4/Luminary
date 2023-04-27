using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_FireWall : Spell
{
    public override void set()
    {
        circle = 3;
        types = 1;
        cd = 8f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
