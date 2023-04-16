using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    // Get Spell in Command pattern
    private Spell skillCommand = null;

    // Set Spell command target
    public void setCommand(Spell command)
    {
        skillCommand = command;
    }
    // Set Command to Null
    public void deSetCommand()
    {
        skillCommand = null;
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

    // Returning skillCommand
    public Spell isSet()
    {
        return skillCommand;
    }
}
