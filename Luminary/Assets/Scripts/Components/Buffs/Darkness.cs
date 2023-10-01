using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : Buff
{
    public Darkness(Charactor tar, Charactor atk) : base(tar, atk)
    {
    }   

    public override bool checkCombinate()
    {
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
