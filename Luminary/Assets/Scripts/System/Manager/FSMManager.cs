using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class FSMManager
{
    public Dictionary<string, List<string>> fsm;
    
    public void init()
    {
        fsm = new Dictionary<string, List<string>>();
        List<string> playerstate = new List<string>
        {
            "PlayerIdleState",
            "PlayerMoveState",
            "PlayerRollState",
            "PlayerCastingState",
            "PlayerDieState",
            "PlayerHitState",
        };
        // Idle State FSM
        List<string> playerfsm = new List<string>
        {
            playerstate[1], playerstate[2], playerstate[3], playerstate[4], playerstate[5],
        };
        fsm[playerstate[0]] = playerfsm;
        
        // Move State FSM
        playerfsm = new List<string>
        {
            playerstate[1], playerstate[2], playerstate[3], playerstate[4], playerstate[5],
        };
        fsm[playerstate[1]] = playerfsm;

        // Roll State FSM
        playerfsm = new List<string>();
        fsm[playerstate[2]] = playerfsm;

        // Casting State FSM
        fsm[playerstate[3]] = playerfsm;
        // Die State FSM
        fsm[playerstate[4]] = playerfsm;
        // Hit State FSM
        fsm[playerstate[5]] = playerfsm;

        List<string> mobState = new List<string>
        {
            "MobIdleState",
            "MobChaseState",
            "MobRunState",
            "MobCastState",
            "MobATKState",
            "MobHitState",
            "MobDieState",
        };

        // Mob Idle State FSM
        List<string> mobFSM = new List<string>
        {
            mobState[1], mobState[2], mobState[3], mobState[4], mobState[5], mobState[6],
        };
        fsm[mobState[0]] = mobFSM;
        

    }

    public List<string> getList(string str)
    {
        return fsm[str];
    }

}
