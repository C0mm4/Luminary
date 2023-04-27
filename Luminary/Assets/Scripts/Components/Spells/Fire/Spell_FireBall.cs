using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_FireBall : Spell
{
    public override void set()
    {
        circle = 2;
        types = 1;
        cd = 3f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
