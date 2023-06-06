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
                GameObject tar = GetClosestObjectToMouse();
                Debug.Log(tar);
                if (tar != null)
                {
                    Debug.Log("Target isn't null");
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
        if (skillCommand != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject GetClosestObjectToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

        float closestDistance = float.MaxValue;
        GameObject closestObject = null;

        if(colliders.Length == 0)
        {
            Debug.Log("Doesn't Colliding");
        }

        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Mob")
            {
                float distance = Vector2.Distance(collider.transform.position, mousePosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = collider.gameObject;
                }
            }

        }

        return closestObject;
    }
}