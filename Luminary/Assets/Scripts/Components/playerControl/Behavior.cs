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
//        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        Vector3 dir = new Vector3(targetPos.x - transform.position.x, targetPos.y - transform.position.y, targetPos.z - transform.position.z);
        dir.Normalize();
        GetComponent<Rigidbody2D>().velocity = dir * speed;
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
        SkillSlot spellw;
        float cd = 0f;
        spellw = GameManager.SkillSlot.getSlot(2).GetComponent<SkillSlot>();
        if (spellw.isSet() != null)
        {
            cdQ = true;
            spellw.useSkill();
            cd = spellw.getCD();
        }

        yield return new WaitForSeconds(cd);
        cdW = false;
        //쿨다운 완료
    }

    public IEnumerator E()
    {
        SkillSlot spelle;
        float cd = 0f;
        spelle = GameManager.SkillSlot.getSlot(1).GetComponent<SkillSlot>();
        if (spelle.isSet() != null)
        {
            cdQ = true;
            spelle.useSkill();
            cd = spelle.getCD();
        }

        yield return new WaitForSeconds(cd);
        cdE = false;
        //쿨다운 완료
    }
    public IEnumerator R()
    {
        SkillSlot spellr;
        float cd = 0f;
        spellr = GameManager.SkillSlot.getSlot(1).GetComponent<SkillSlot>();
        if (spellr.isSet() != null)
        {
            cdQ = true;
            spellr.useSkill();
            cd = spellr.getCD();
        }

        yield return new WaitForSeconds(cd);
        cdR = false;
        //쿨다운 완료
    }
}