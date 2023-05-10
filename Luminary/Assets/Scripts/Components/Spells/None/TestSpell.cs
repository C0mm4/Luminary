using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TestSpell : MonoBehaviour 
{
    [SerializeField]
    GameObject target;

    public void Start()
    {
        execute();
    }

    public void execute()
    {
        // Running This Spell
        target = GameObject.FindGameObjectWithTag("Player");
        run();
    }
    
    public void run()
    {
        // Accelerated Player Speed
        target.GetComponent<Behavior>().speedIncrease += 10f;

        // After 0.3f seconds rollback Player Speed
        Invoke("endrun", 0.3f);
    }
    
    public void endrun()
    {
        target.GetComponent<Behavior>().speedIncrease -= 10f;
        GameManager.Resource.Destroy(this.gameObject);
    }

   
}
