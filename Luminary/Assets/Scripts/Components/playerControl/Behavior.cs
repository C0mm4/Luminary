using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
 
public class Behavior : Control
{
    [SerializeField]
    public float speed = 5f;

    [SerializeField]
    private GameObject[] spellPrefab;

    [SerializeField]
    private GameObject[] skillslots;


    public void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
    
    public IEnumerator roll()
    {
        SkillSlot spellRoll;
        float cd = 0f;
        spellRoll = GameManager.SkillSlot.getSlot(0).GetComponent<SkillSlot>();
        if(spellRoll.isSet() != null)
        {
            cdRoll = true;
            spellRoll.useSkill();
            cd = spellRoll.getCD();
        }

        yield return new WaitForSeconds(cd);
        cdRoll = false;
        yield return 0;
    }
    
    public IEnumerator Q()
    {
        SkillSlot spellq;
        float cd = 0f;
        spellq = GameManager.SkillSlot.getSlot(1).GetComponent<SkillSlot>();
        if (spellq.isSet() != null)
        {
            cdQ = true;
            spellq.useSkill();
            cd = spellq.getCD();
        }
        
        yield return new WaitForSeconds(cd);
        cdQ = false;
        //쿨다운 완료
    }

    public IEnumerator W()
    {
        SkillSlot testobj;
        float cd = 0f;
        testobj = skillslots[2].GetComponent<SkillSlot>();
        if (testobj.isSet() != null)
        {
            testobj.useSkill();
            cd = testobj.getCD();
        }

        yield return new WaitForSeconds(cd);
        //쿨다운 완료
    }

    public IEnumerator E()
    {
        SkillSlot testobj;
        float cd = 0f;
        testobj = skillslots[3].GetComponent<SkillSlot>();
        if (testobj.isSet() != null)
        {
            testobj.useSkill();
            cd = testobj.getCD();
        }

        yield return new WaitForSeconds(cd);
        //쿨다운 완료
    }
    public IEnumerator R()
    {
        SkillSlot testobj;
        float cd = 0f;
        testobj = skillslots[4].GetComponent<SkillSlot>();
        if (testobj.isSet() != null)
        {
            testobj.useSkill();
            cd = testobj.getCD();
        }

        yield return new WaitForSeconds(cd);
        //쿨다운 완료
    }
}