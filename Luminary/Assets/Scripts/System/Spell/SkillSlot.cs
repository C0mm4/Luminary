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
        switch (skillCommand.data.type)
        {
            case 0:
            case 1:
            case 3:
                Vector3 pos = GameManager.inputManager.mouseWorldPos;

                GameManager.player.GetComponent<Charactor>().changeState(new PlayerCastingState(getSpell(), GameManager.inputManager.mouseWorldPos));
                skillCommand.isCool = true;
                skillCommand.st = Time.time;
                break;
            case 2:
                GameObject tar = getMousePosObj();
                if(tar != null)
                {
                    GameManager.player.GetComponent<Charactor>().changeState(new PlayerCastingState(getSpell(), tar));
                    skillCommand.isCool = true;
                    skillCommand.st = Time.time;
                }
                break;
        }

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

    public GameObject getMousePosObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(GameManager.inputManager.mousePos);

        List<RaycastHit> hits = new List<RaycastHit>();

        RaycastHit[] allHits = Physics.RaycastAll(ray);
        hits.AddRange(allHits);

        hits.Sort((hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        foreach (RaycastHit hit in hits)
        {
            GameObject closestObject = hit.collider.gameObject;
            if (closestObject.CompareTag("Mob"))
            {
                return closestObject;
            }
        }

        return null;
    }

}
