using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot
{
    // Get Spell in Command pattern
    private Spell skillCommand = null;

    public SkillSlotUI ui;



    public SkillSlot()
    {
        ui = GameManager.Instance.uiManager.skillSlotUI.GetComponent<SkillSlotUI>();
    }

    // Set Spell command target
    public void setCommand(Spell command)
    {
        skillCommand = command;
        ui.setImg();
    }
    // Set Command to Null
    public void deSetCommand()
    {
        skillCommand = null;
        ui.setImg();
    }

    public Spell getSpell()
    {
        return skillCommand;
    }

    // Use Spell in triggered
    public void useSkill()
    {
        skillCommand.execute();
        skillCommand.isCool = true;
        skillCommand.st = Time.time;
    }

    // return Spell Cooltime 
    public float getCD()
    {
        return skillCommand.getCD();
    }

    // Returning skillCommand
    public bool isSet()
    {
        if(skillCommand != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
