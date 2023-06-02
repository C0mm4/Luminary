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
        player.GetComponent<Charactor>().status.increaseSpeed += 3;
        player.GetComponent<Charactor>().calcStatus();
    }

    
    public void run()
    {
        // Accelerated Player Speed
        State getspell = player.GetComponent<Charactor>().getState();
        if (getspell.GetType().Name == "PlayerMoveState")
        {
            player.GetComponent<Charactor>().changeState(new PlayerRollState());
            // After 0.3f seconds rollback Player Speed
            Invoke("endrun", 0.3f);
        }
        else
        {
            player.GetComponent<Charactor>().status.increaseSpeed -= 3;
            GameManager.Resource.Destroy(this.gameObject);
        }
    }
    
    public void endrun()
    {
        player.GetComponent<Charactor>().status.increaseSpeed -= 3;
        player.GetComponent<Charactor>().calcStatus();
        player.GetComponent<Charactor>().endCurrentState();
        GameManager.Resource.Destroy(this.gameObject);
    }

}