using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    // Get Spell in Command pattern
    private Spell skillCommand = null;
    public SlotImg slotImg = null;
    spellFill spf = null;

    // Set Spell command target
    public void setCommand(Spell command)
    {
        slotImg = GetComponentInChildren<SlotImg>();
        spf = GetComponentInChildren<spellFill>();
        skillCommand = command;
        setImg();
    }
    // Set Command to Null
    public void deSetCommand()
    {
        skillCommand = null;
        slotImg.deSetImg();
    }

    public Spell getSpell()
    {
        return skillCommand;
    }

    // Use Spell in triggered
    public void useSkill()
    {
        skillCommand.execute();
        spf.setCooltime(skillCommand.getCD());
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

    public void setImg()
    {
        slotImg.setImg(skillCommand.spr);
    }

}
