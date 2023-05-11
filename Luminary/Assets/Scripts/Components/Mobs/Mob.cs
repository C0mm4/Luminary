using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : Charactor
{
    // Mob Attack Prefab
    [SerializeField]
    public GameObject[] attackPrefab;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        sMachine = new StateMachine();
        sMachine.changeState(new MobIdleState(), this);
        player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();
    }
}
