using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TestSpell : Spell
{
    GameObject obj = null;
    float originspd;

    public override void execute()
    {
        // Setting Spell's Cooltime
        cd = 1f;

        // Searching Player Object
        obj = GameObject.Find("sampleChara");
        
        // Running This Spell
        run();
    }

    public void run()
    {
        // Finding Original Player Speed
        originspd = obj.GetComponent<Behavior>().speed;

        // Accelerated Player Speed
        obj.GetComponent<Behavior>().speed = 10f;

        // After 0.3f seconds rollback Player Speed
        Invoke("endrun", 0.3f);
    }
    
    public void endrun()
    {
        obj.GetComponent<Behavior>().speed = originspd;
    }

}
