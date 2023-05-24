using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
 
public class Behavior : Control
{
    [SerializeField]
    private GameObject[] spellPrefab;


    public SkillSlot[] skillSlots;
    [SerializeField]
    Vector3 mos;

    public override void Start()
    {
        base.Start();
        skillSlots = new SkillSlot[5];
        setSkillSlots();
        Debug.Log("PlayerGen");
        Debug.Log(GameManager.Spells.spells[1].data.name);
        Debug.Log(GameManager.Spells.spells[2].data.name); 
        skillSlots[0].setCommand(GameManager.Spells.spells[1]);
        skillSlots[1].setCommand(GameManager.Spells.spells[2]);
    }

    public override void Update()
    {
        base.Update();
        mos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        CheckCDs();
    }

    public void CheckCDs()
    {
        foreach(SkillSlot slot in skillSlots)
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

    private void setSkillSlots()
    {
        skillSlots[0] = new SkillSlot();
        skillSlots[1] = new SkillSlot();
        skillSlots[2] = new SkillSlot();
        skillSlots[3] = new SkillSlot();
        skillSlots[4] = new SkillSlot();
    }

    public void move()
    {
//        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        Vector3 dir = new Vector3(targetPos.x - transform.position.x, targetPos.y - transform.position.y, targetPos.z - transform.position.z);
        dir.Normalize();
        GetComponent<Rigidbody2D>().velocity = dir * (speed + speedIncrease);
    }
    
    public IEnumerator roll()
    {
        float cd = 0f;
        if (skillSlots[0].isSet())
        {
            cdRoll = true;
            skillSlots[0].useSkill();
            cd = skillSlots[0].getCD();
        }

        yield return new WaitForSeconds(cd);
        cdRoll = false;
        yield return 0;
    }
    
    public IEnumerator Q()
    {

        float cd = 0f;
        if (skillSlots[1].isSet())
        {
            cdQ = true;
            skillSlots[1].useSkill();
            cd = skillSlots[1].getCD();
        }

        yield return new WaitForSeconds(cd);
        cdQ = false;
        yield return 0;
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
        cdW = false;
        //쿨다운 완료
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
        cdE = false;
        //쿨다운 완료
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
        cdR = false;
        //쿨다운 완료
    }

    
}