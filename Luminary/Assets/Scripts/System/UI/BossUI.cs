using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Boss boss;
    public Image HPBar;
    public Image HP;

    [SerializeField]
    public int targetMaxHP;
    public int targetCurrentHP;
    public int yellowBar;

    public float lastDmgT;
    public bool yellowbarRunning = false;

    // Update is called once per frame
    void Update()
    {
        if(targetCurrentHP != boss.status.currentHP)
        {
            lastDmgT = Time.time;
            SetHP();
        }
        if(Time.time - lastDmgT > 2f)
        {
            if(!yellowbarRunning)
            {
                StartCoroutine(SetYellowBar(targetCurrentHP));
            }
        }
    }

    public void SetData()
    {
        targetCurrentHP = boss.status.currentHP;
        yellowBar = targetCurrentHP;
        targetMaxHP = boss.status.maxHP;
        lastDmgT = Time.time;
        yellowbarRunning = false;
        StartCoroutine(HPFill());
    }

    public void SetHP()
    {
        targetCurrentHP = boss.status.currentHP;
        HP.fillAmount = (float)((float)boss.status.currentHP / (float)boss.status.maxHP);

    }

    public IEnumerator SetYellowBar(int targetYellowBar)
    {
        yellowbarRunning = true;
        for(; yellowBar > targetYellowBar; yellowBar--)
        {
            HPBar.fillAmount = (float)((float)yellowBar / (float)boss.status.maxHP);
            yield return new WaitForSeconds(0.0001f);
        }
        yellowbarRunning = false;
    }

    public IEnumerator HPFill()
    {
        for(int i = 0; i < 100; i++)
        {
            HP.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.001f);

        }
    }
}
