using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : Charactor
{
    // Mob Attack Prefab
    [SerializeField]
    public GameObject[] attackPrefab;
    public MobData data;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        sMachine.changeState(new MobIdleState());
        player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();
        
    }

    public override void statusInit()
    {
        status.baseHP = data.baseHP;
        status.baseDMG = data.baseDMG;
        status.basespeed = data.basespeed;

        base.statusInit();
    }

}
