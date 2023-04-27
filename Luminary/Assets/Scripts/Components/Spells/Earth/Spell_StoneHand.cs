using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_StoneHand : Spell
{
    public override void set()
    {
        circle = 3;
        types = 4;
        cd = 5f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
    }

    public override void execute()
    {
    }
}
