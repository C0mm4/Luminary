using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TestSpell : SpellObj 
{

    public override void Start()
    {
        base.Start();
        run();
    }

    
    public void run()
    {
        // Accelerated Player Speed
        player.GetComponent<Behavior>().status.increaseSpeed += 10;
        player.GetComponent<Charactor>().calcStatus();
        // After 0.3f seconds rollback Player Speed
        Invoke("endrun", 0.3f);
    }
    
    public void endrun()
    {
        player.GetComponent<Behavior>().status.increaseSpeed -= 10;
        player.GetComponent<Charactor>().calcStatus();
        GameManager.Resource.Destroy(this.gameObject);
    }

}