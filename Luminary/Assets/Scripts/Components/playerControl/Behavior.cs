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
    private GameObject[] slots;
    public SkillSlot testslot;
    

    public void Start()
    {
        
    }

    public void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
    
    public IEnumerator roll()
    {
        
        Spell cmd;
        GameObject testobj = new GameObject("spellroll");
        testobj.AddComponent<TestSpell>();
        cmd = testobj.GetComponent<TestSpell>();
        testslot = slots[0].GetComponent<SkillSlot>();
        if(testslot != null )
        {
            testslot.setCommand(cmd);
        }
        else
        {
            Debug.Log("cmd is NULL");
        }
        if (testslot != null)
        {
            testslot.useSkill();
            cdRoll = true;
            Debug.Log(testslot.getCD());
            yield return new WaitForSeconds(testslot.getCD());
            cdRoll = false;
            //쿨다운 완료
        }
        

    }
    
    public IEnumerator Q()
    {

        
        CastSpellQ();
        cdQ = true;
        //스킬 발사 이후 스킬 쿨다운

        yield return new WaitForSeconds(2);
        cdQ = false;
        //쿨다운 완료
    }

    public IEnumerator W()
    {
        CastSpellW();
        cdW = true;
        //스킬 발사 이후 스킬 쿨다운

        yield return new WaitForSeconds(2);
        cdW = false;
        //쿨다운 완료
    }

    public IEnumerator E()
    {
        CastSpellE();
        cdE = true;
        //스킬 발사 이후 스킬 쿨다운

        yield return new WaitForSeconds(2);
        cdE = false;
        //쿨다운 완료
    }

    public void CastSpellQ()
    {
        Instantiate(spellPrefab[0], transform.position, Quaternion.identity);
    }

    public void CastSpellW()
    {
        Instantiate(spellPrefab[1], transform.position, Quaternion.identity);
    }

    public void CastSpellE()
    {
        Instantiate(spellPrefab[2], transform.position, Quaternion.identity);
    }
}