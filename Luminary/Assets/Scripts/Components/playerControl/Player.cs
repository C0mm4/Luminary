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
        skillslots[1].setCommand(GameManager.Spells.spells[2]);

        status.basespeed = 5f;
        calcStatus();

        GameManager.inputManager.KeyAction += onKeyboard;
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
        GameManager.inputManager.KeyAction -= onKeyboard;
        GameManager.Instance.SceneChangeAction -= DieObject;
        
        base.DieObject();
    }

    public override void Update()
    {
        base.Update();
        if(getState() == null)
        {
            changeState(new PlayerIdleState());
        }
        CheckCDs();
    }

    public void onKeyboard()
    {
        if (Input.GetMouseButton(1))
        {
            sMachine.changeState(new PlayerMoveState());
        }
        
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

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

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
        if (skillslots[0].isSet())
        {
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
    public IEnumerator Q()
    {

        float cd = 0f;
        if (skillslots[1].isSet())
        {
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

    public IEnumerator W()
    {
        SkillSlot spellw;
        float cd = 0f;/*
        spellw = GameManager.SkillSlot.getSlot(2).GetComponent<SkillSlot>();
        if (spellw.isSet() != null)
        {
            cdQ = true;
            spellw.useSkill();
            cd = spellw.getCD();
        }*/

        yield return new WaitForSeconds(cd);
        //Äð´Ù¿î ¿Ï·á
    }

    public IEnumerator E()
    {
        SkillSlot spelle;
        float cd = 0f;/*
        spelle = GameManager.SkillSlot.getSlot(3).GetComponent<SkillSlot>();
        if (spelle.isSet() != null)
        {
            cdQ = true;
            spelle.useSkill();
            cd = spelle.getCD();
        }*/

        yield return new WaitForSeconds(cd);
        //Äð´Ù¿î ¿Ï·á
    }
    public IEnumerator R()
    {
        SkillSlot spellr;
        float cd = 0f;/*
        spellr = GameManager.SkillSlot.getSlot(4).GetComponent<SkillSlot>();
        if (spellr.isSet() != null)
        {
            cdQ = true;
            spellr.useSkill();
            cd = spellr.getCD();
        }
*/
        yield return new WaitForSeconds(cd);
        //Äð´Ù¿î ¿Ï·á
    }


}
