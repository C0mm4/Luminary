using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellFill : MonoBehaviour
{
    public Image img;

    private float cooltime, timeStart, timeCurrent;
    private bool isRun = false;

    private void Start()
    {
        img.color = new Color(0, 0, 0, 0);
        img.fillAmount = 0;
    }
    // Update is called once per frame
    void Update()
    {

        if (isRun)
        {
            checkCoolTime();
        }
    }

    public void setCooltime(float cd)
    {
        cooltime = cd;
        startSpell();
    }

    public void startSpell()
    {
        timeStart = Time.time;
        img.color = new Color(0, 0, 0, 0.8f);
        isRun = true;
    }

    void checkCoolTime()
    {
        timeCurrent = Time.time - timeStart;
        if(timeCurrent < cooltime)
        {
            img.fillAmount = timeCurrent/cooltime;
        }
        else
        {
            isRun = true;
            img.fillAmount = 0;
            img.color = new Color(0, 0, 0, 0);
        }
    }
}
