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
        State getspell = player.GetComponent<Charactor>().getState();
        if (getspell.GetType().Name == "PlayerMoveState")
        {
            Debug.Log("Player Moving");
            player.GetComponent<Charactor>().changeState(new PlayerRollState());
            player.GetComponent<Charactor>().status.increaseSpeed += 10;
            player.GetComponent<Charactor>().calcStatus();
            // After 0.3f seconds rollback Player Speed
            Invoke("endrun", 0.3f);
        }
        else
        {
            Debug.Log("Player Doesn't Move");
        }
    }
    
    public void endrun()
    {
        Debug.Log(player);
        player.GetComponent<Charactor>().status.increaseSpeed -= 10;
        player.GetComponent<Charactor>().calcStatus();
        GameManager.Resource.Destroy(this.gameObject);
    }

}