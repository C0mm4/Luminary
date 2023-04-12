using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Behavior : Control
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    private GameObject[] spellPrefab;

    public void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    public IEnumerator roll()
    {
        speed = 10f;
        yield return new WaitForSeconds(0.15f);
        //0.3초 지속

        speed = 5f;
        cdRoll = true;
        //구르기 이후 속도 원복 후 구르기 쿨다운

        yield return new WaitForSeconds(1);
        cdRoll = false;
        //쿨다운 완료
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