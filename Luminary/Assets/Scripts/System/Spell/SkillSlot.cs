using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    // Get Spell in Command pattern
    private Spell skillCommand;

    // Set Spell command target
    public void setCommand(Spell command)
    {
        skillCommand = command;
    }

    // Use Spell in triggered
    public void useSkill()
    {
        skillCommand.execute();
    }

    // return Spell Cooltime 
    public float getCD()
    {
        return skillCommand.getCD();
    }
}
