using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Mob
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        isboss = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
