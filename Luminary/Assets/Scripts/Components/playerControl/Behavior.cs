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
    public SkillSlot testslot;

    [SerializeField]
    private GameObject[] inventory;
    
    /*
    public void Start()
    {
        skillslots = new GameObject[5];
        for(int i = 0; i < skillslots.Length; i++)
        {
            Debug.Log(i);
            GameObject go = GameManager.Resource.Instantiate("SkillSlot");
            skillslots[i] = go;
        }
    }*/

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
            spellRoll.useSkill();
            cd = spellRoll.getCD();
        }

        yield return new WaitForSeconds(cd);
    }
    
    public IEnumerator Q()
    {
        SkillSlot spellq;
        float cd = 0f;
        spellq = skillslots[1].GetComponent<SkillSlot>();
        if (spellq.isSet() != null)
        {
            spellq.useSkill();
            cd = spellq.getCD();
        }
        
        yield return new WaitForSeconds(cd);
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