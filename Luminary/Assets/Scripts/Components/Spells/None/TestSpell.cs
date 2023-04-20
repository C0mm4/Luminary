using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TestSpell : Spell
{
    float originspd;

    public override void set()
    {
        
        // Setting Spell's Cooltime
        cd = 1f;
        types = 5;
        circle = 0;

        // Searching Player Object
        obj = GameObject.Find("PlayerbleChara");
        
    }

    public override void execute()
    {
        base.execute();
        // Setting Spell Components
        set();
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
