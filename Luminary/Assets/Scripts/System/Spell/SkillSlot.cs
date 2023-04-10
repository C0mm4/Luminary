using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : Button
{
    private Command skillCommand;

    public void setCommand(Command command)
    {
        skillCommand = command;
    }

    public void useSkill()
    {
        skillCommand.execute();
    }

}
