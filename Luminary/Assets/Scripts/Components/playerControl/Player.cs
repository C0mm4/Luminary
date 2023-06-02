using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : Charactor
{
    // Start is called before the first frame update

    public SkillSlot[] skillslots;


    public override void Awake()
    {
        base.Awake();
        player = this;
        GameManager.player = player.gameObject;

        skillslots = new SkillSlot[5];
        setSkillSlots();


        skillslots[0].setCommand(GameManager.Spells.spells[1]);
        skillslots[1].setCommand(GameManager.Spells.spells[1003000]);

        status.basespeed = 5f;
        calcStatus();

        GameManager.Instance.SceneChangeAction += DieObject;
        sMachine.changeState(new PlayerIdleState());
    }
    private void setSkillSlots()
    {
        skillslots[0] = new SkillSlot();
        skillslots[1] = new SkillSlot();
        skillslots[2] = new SkillSlot();
        skillslots[3] = new SkillSlot();
        skillslots[4] = new SkillSlot();
    }

    public override void DieObject()
    {
        GameManager.inputManager.KeyAction -= moveKey;
        GameManager.inputManager.KeyAction -= spellKey;
        GameManager.Instance.SceneChangeAction -= DieObject;
        
        base.DieObject();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(getState() == null)
        {
            changeState(new PlayerIdleState());
        }
        CheckCDs();
    }

    public void moveKey()
    {
        if (Input.GetMouseButton(1))
        {
            sMachine.changeState(new PlayerMoveState());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (skillslots[0].isSet())
            {
                if (!skillslots[0].getSpell().isCool)
                {
                    StartCoroutine("roll");
                }
            }
        }
        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            Debug.Log("Interection Key pressed");
            GameManager.Instance.interaction();
        }
    }

    public void spellKey()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (skillslots[1].isSet())
            {
                if (!skillslots[1].getSpell().isCool)
                    StartCoroutine("Q");
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (skillslots[2].isSet())
            {
                if (!skillslots[2].getSpell().isCool)
                    StartCoroutine("W");
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (skillslots[3].isSet())
            {
                if (!skillslots[3].getSpell().isCool)
                    StartCoroutine("E");
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (skillslots[4].isSet())
            {
                if (!skillslots[4].getSpell().isCool)
                    StartCoroutine("R");
            }
        }
    }

    public void CheckCDs()
    {
        foreach (SkillSlot slot in skillslots)
        {
            if (slot.isSet())
            {
                if (slot.getSpell().isCool)
                {
                    slot.getSpell().ct = Time.time - slot.getSpell().st;
                    if (slot.getSpell().ct > slot.getSpell().getCD())
                    {
                        slot.getSpell().isCool = false;
                    }
                }
            }
        }
    }

    public IEnumerator roll()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState")) 
        {
            if (skillslots[0].isSet())
            {
                changeState(new PlayerCastingState(skillslots[0].getSpell(), GameManager.inputManager.mouseWorldPos));
                skillslots[0].getSpell().isCool = true;
                skillslots[0].useSkill();
                cd = skillslots[0].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[0].isSet())
            {
                skillslots[0].getSpell().isCool = false;
            }
        }

    }
    public IEnumerator Q()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[1].isSet())
            {
                changeState(new PlayerCastingState(skillslots[1].getSpell(), GameManager.inputManager.mouseWorldPos));
                skillslots[1].getSpell().isCool = true;
                skillslots[1].useSkill();
                cd = skillslots[1].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[1].isSet())
            {
                skillslots[1].getSpell().isCool = false;
            }
        }

    }

    public IEnumerator W()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[2].isSet())
            {
                changeState(new PlayerCastingState(skillslots[2].getSpell(), GameManager.inputManager.mouseWorldPos));
                skillslots[2].getSpell().isCool = true;
                skillslots[2].useSkill();
                cd = skillslots[2].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[2].isSet())
            {
                skillslots[2].getSpell().isCool = false;
            }
        }
    }

    public IEnumerator E()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[1].isSet())
            {
                changeState(new PlayerCastingState(skillslots[3].getSpell(), GameManager.inputManager.mouseWorldPos));
                skillslots[3].getSpell().isCool = true;
                skillslots[3].useSkill();
                cd = skillslots[3].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[3].isSet())
            {
                skillslots[3].getSpell().isCool = false;
            }
        }
    }
    public IEnumerator R()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[1].isSet())
            {
                changeState(new PlayerCastingState(skillslots[4].getSpell(), GameManager.inputManager.mouseWorldPos));
                skillslots[4].getSpell().isCool = true;
                skillslots[4].useSkill();
                cd = skillslots[4].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[4].isSet())
            {
                skillslots[4].getSpell().isCool = false;
            }
        }
    }


}
